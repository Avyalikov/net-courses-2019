using HtmlAgilityPack;
using MultithreadApp.Core.Models;
using MultithreadApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace MultithreadApp.Core.Services
{
    public class PageService 
    {
        private readonly IPageTableRepository pageRepository;
        public PageService (IPageTableRepository pageRepository)
        {
            this.pageRepository = pageRepository; 
        }

        public PageService()
        {
        }

        private string fileName = "WebPageFile.txt";
        public string DownLoadPage(string url)
        {
            try
            {
                WebClient client = new WebClient();
                string Page = client.DownloadString(url);
                try
                {
                    StreamWriter SW = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
                    SW.Write(Page);
                    SW.Close();
                }
                catch
                {
                    throw new ArgumentException($"Can't save downloaded page from {url} to file {fileName}");
                }
            }
            catch
            {
                throw new ArgumentException($"Can't download url {url}");
            }

            return fileName;
        }

        public void Add(string item)
        {
            var entityToAdd = new PageEntity()
            {
                Link = item,
                IterationId = 1
            };
            if (this.pageRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This user has been already registered.Can't continue");
            }
            this.pageRepository.Add(entityToAdd);

            this.pageRepository.SaveChanges();
        }

        public List<string> ExtractHtmlTags(string fileName)
        {
            List<string> hrefList = new List<string>();
            var doc = new HtmlDocument();
            doc.Load(fileName);

            var links = doc.DocumentNode.Descendants("a");
            foreach (var node in links)
            {
                var href = node.GetAttributeValue("href", string.Empty);
                hrefList.Add(href);
            }
            return hrefList;
        }
    }
}
