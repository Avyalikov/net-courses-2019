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
        private ILinkTableRepository linkTableRepository;
        static object linksTableLocker = new object();

        public ParsingService(ILinkTableRepository linkTableRepository)
        {
            this.linkTableRepository = linkTableRepository;
        }

        public async Task<string> DownloadPage(string link, HttpMessageHandler handler, int id)
        {
            if (handler == null)
            {
                var defaultClientHandler = new HttpClientHandler();
                defaultClientHandler.UseDefaultCredentials = true;
                handler = defaultClientHandler;
            }

            string filePath = $@"LinkFiles\{id}.txt";

            using (var client = new HttpClient(handler))
            {
                using (var response = await client.GetAsync(link))
                {
                    using (var content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();                        

                        using (FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            // convert string to bytes
                            byte[] array = System.Text.Encoding.Default.GetBytes(jsonString);
                            // record byte array to file
                            await fstream.WriteAsync(array, 0, array.Length);                            

                            return filePath;
                        }                        
                    }
                }
            }
        }
        
        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// Sample from https://habr.com/ru/post/273807/
        /// </summary>
        public List<string> ExtractLinksFromHtmlString(ref string[] startPageHosts, string htmlContentFilePath)
        {
            string content;

            using (StreamReader sr = new StreamReader(htmlContentFilePath))
            {
                content = sr.ReadToEnd();
            }

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
            var filePathTask = this.DownloadPage(link, null, id); // Warnung!!! need to check for deadlock
            filePathTask.Wait();
            // Get links list from file
            var extractlinksList = this.ExtractLinksFromHtmlString(ref startPageHosts, filePathTask.Result);

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

        public Dictionary<string, int> LookingForDuplicatesInDb()
        {
            var list = this.linkTableRepository.LookingForDuplicateLinkStrings();
            if (list == null)
            {
                list = new Dictionary<string, int>();
            }
            return list;
        }
    }
}
