namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;

    public class ParsingService
    {
        private static readonly object linksTableLocker = new object();

        private readonly ILinkTableRepository linkTableRepository;
        private readonly IFileManager fileManager;

        public ParsingService(ILinkTableRepository linkTableRepository, IFileManager fileManager)
        {
            this.linkTableRepository = linkTableRepository;
            this.fileManager = fileManager;
        }

        public async Task<string> DownloadPage(string link, HttpMessageHandler handler, int id)
        {
            bool downloadResult;

            if (handler == null)
            {
                handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true
                };
            }

            string filePath = $@"LinkFiles\{id}.txt";

            using (var client = new HttpClient(handler))
            {
                HttpResponseMessage response = null;

                try
                {
                    response = await client.GetAsync(link);
                }
                catch
                {
                    throw;
                }
                if (response == null)
                {
                    throw new HttpRequestException("Cannot get answer from website");
                }
                if (response.IsSuccessStatusCode)
                {
                    downloadResult = await LoadIntoFile(filePath, response);
                    return filePath;
                }
                else
                {
                    throw new HttpRequestException("Bad response");
                }
            }
        }

        public async Task<bool> LoadIntoFile(string filePath, HttpResponseMessage resp)
        {
            //using (FileStream fstream = this.fileManager.FileStream(filePath, FileMode.OpenOrCreate))
            //{
            //    var jsonString = await content.ReadAsStringAsync();

            //    // convert string to bytes
            //    byte[] array = System.Text.Encoding.Default.GetBytes(jsonString);
            //    // record byte array to file
            //    await fstream.WriteAsync(array, 0, array.Length);
            //}

            using (Stream contentStream = await resp.Content.ReadAsStreamAsync(),
                    stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                await contentStream.CopyToAsync(stream);
            }

            if (File.Exists(filePath))
            {
                return true;
            }
            throw new ArgumentException("Cannot create file");
        }

        public string LoadFromFile(string htmlContentFilePath)
        {
            string content;

            using (StreamReader sr = this.fileManager.StreamReader(htmlContentFilePath))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }
        
        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// Sample from https://habr.com/ru/post/273807/
        /// </summary>
        public List<string> ExtractLinksFromHtmlString(ref string[] startPageHosts, string content)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(content);

            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                for (int i = 1; i < startPageHosts.Length; i++)
                {
                    if (att.Value.StartsWith(startPageHosts[i]))
                    {
                        hrefTags.Add(att.Value);
                    }
                }                
            }

            return hrefTags;
        }

        public int Save(string link, int iterationId)
        {
            SaveValidation(link, iterationId);

            ContainsByLink(link);

            var entityToAdd = new LinkEntity()
            {
                Link = link,
                IterationId = iterationId
            };

            this.linkTableRepository.Add(entityToAdd);

            this.linkTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void SaveValidation(string link, int iterationId)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentException("'link' is null, empty or consists only of white-space characters");
            }

            if (iterationId < 0)
            {
                throw new ArgumentException("'iterationId' is negative");
            }
        }

        public void ParsingLinksByIterationId(string link, int id, string[] startPageHosts, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            // Async Download link content. Create id.txt file                
            var filePathTask = DownloadPage(link, null, id);
            filePathTask.Wait();
            //Load data from file
            string content = LoadFromFile(filePathTask.Result);
            // Get links list from file
            var extractlinksList = this.ExtractLinksFromHtmlString(ref startPageHosts, content);

            // Remove file
            FileInfo fileInf = new FileInfo(filePathTask.Result);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }

            // For each extract link in list...
            foreach (var extractLink in extractlinksList)
            {
                try
                {
                    // Save extractLink to DB and get her Id
                    int newIterationId;
                    lock (linksTableLocker)
                    {
                        newIterationId = this.Save(startPageHosts[0] + extractLink, id);
                    }                    
                }
                catch (ArgumentException e)
                {
                    // If find link in DB, write message
                    var message = e.Message;
                }
            }
            extractlinksList.Clear();

            if (!cancellationToken.IsCancellationRequested)
            {
                List<Task> parsingTasks = new List<Task>();

                // Get Entity list from DB by operationId
                var entityList = new List<LinkEntity>();
                lock (linksTableLocker)
                {
                    entityList = this.linkTableRepository.EntityListByIterationId(id);
                }

                foreach (var linkEntity in entityList)
                {
                    // ver.2
                    // Use this Id as iterationId with recursion
                    parsingTasks.Add(Task.Factory.StartNew(() => ParsingLinksByIterationId(linkEntity.Link, linkEntity.Id, startPageHosts, cancellationToken)));
                }

                Task.WaitAll(parsingTasks.ToArray());
            }
            
        }

        public void ContainsByLink(string link)
        {
            if (this.linkTableRepository.ContainsByLink(link))
            {
                throw new ArgumentException("This link has been registered. Can't continue.");
            }
        }
    }
}
