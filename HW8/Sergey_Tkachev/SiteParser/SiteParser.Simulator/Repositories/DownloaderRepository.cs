using SiteParser.Core.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator.Repositories
{
    class DownloaderRepository : IDownloader
    {
        private readonly SiteParserDbContext dbContext;
        private string pathToFile = "..\\..\\Pages/";

        public DownloaderRepository(SiteParserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string Download(string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(requestUrl).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        return result;
                    }
                }
            }
        }

        public string SaveIntoFile(string downloadedResult)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(downloadedResult);
            htmlDocument.DocumentNode.Descendants("title").FirstOrDefault();
            string fullPath = pathToFile + htmlDocument;
            if (File.Exists(fullPath))
            {
                throw new ArgumentException($"Such file {fullPath} already exists!");
            }

            File.WriteAllText(fullPath, downloadedResult, Encoding.UTF8);

            return fullPath;
        }
    }
}
