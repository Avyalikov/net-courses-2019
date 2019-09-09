using HtmlAgilityPack;
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
        List<string> urls;

        public ParsePageService(SaveIntoDatabaseService saveIntoDatabaseService)
        {
            this.saveIntoDatabaseService = saveIntoDatabaseService;
            this.urls = new List<string>();
        }

        public ICollection<string> Parse(string path, string baseUrl)
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
                if (parsedUrl.Contains("wiki")) {
                    if (!parsedUrl.Contains("http"))
                    {
                        urls.Add(baseUrl + parsedUrl);
                        break;
                    }
                    urls.Add(parsedUrl);
                }
            }
            if (urls.Count != 0)
            {
                saveIntoDatabaseService.SaveUrls(urls);
            }
            return urls;
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
