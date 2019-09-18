using HtmlAgilityPack;
using SiteParser.Core.Models;
using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SiteParser.Core.Services
{
    public class ParsePageService
    {
        private readonly SaveIntoDatabaseService saveIntoDatabaseService;
        private readonly DeleteFileService deleteFileService;
        List<string> urls;
        static private object locker = new object();

        public ParsePageService(SaveIntoDatabaseService saveIntoDatabaseService, DeleteFileService deleteFileService)
        {
            this.saveIntoDatabaseService = saveIntoDatabaseService;
            this.deleteFileService = deleteFileService;
            this.urls = new List<string>();
        }

        public ICollection<string> Parse(string path, string baseUrl, int iterationID)
        {
            urls.Clear();

            HtmlDocument htmlDocument = LoadFromFile(path);

            if(htmlDocument.DocumentNode.SelectNodes("//a[@href]") == null)
            {
                return urls;
            }

            foreach (HtmlNode link in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
            {
                string parsedUrl = link.GetAttributeValue("href", string.Empty);
                if (parsedUrl.Contains("wiki"))
                {
                    if (!parsedUrl.Contains("http"))
                    {
                        if (CheckIfUrlExist(baseUrl + parsedUrl))
                        {
                            break;
                        }
                        urls.Add(baseUrl + parsedUrl);
                        continue;
                    }

                    if (CheckIfUrlExist(baseUrl + parsedUrl))
                    {
                        break;
                    }
                    urls.Add(parsedUrl);
                }
            }

            if (urls.Count != 0)
            {
                lock (locker)
                {
                    saveIntoDatabaseService.SaveUrls(urls, iterationID);
                    deleteFileService.DeleteFile(path);
                }
            }
            return urls;
        }

        private bool CheckIfUrlExist(string urlToCheck)
        {
            if (urls.Contains(urlToCheck))
            {
                return true;
            }
            return false;
        }

        HtmlDocument LoadFromFile(string path)
        {
            var htmlFile = new HtmlDocument();
            try
            {
                htmlFile.Load(path);
            } catch (Exception ex) when(ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                Console.WriteLine(ex.Message);
            }
            return htmlFile;
        }
    }
}
