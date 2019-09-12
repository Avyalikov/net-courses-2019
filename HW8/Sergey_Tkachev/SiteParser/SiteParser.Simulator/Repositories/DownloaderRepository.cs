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
        private string pathToFile = string.Empty;
        private string folder = "Pages";

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
            this.pathToFile = DirectoryCheck(this.folder);

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(downloadedResult);
            var title = htmlDocument.DocumentNode.SelectSingleNode("//head/title");

            string fileName = title.InnerText;
            FileNameCheck(ref fileName);
            string fullPath = Path.Combine(pathToFile, fileName);

            if (File.Exists(fullPath))
            {
                throw new ArgumentException($"Such file {fullPath} already exists!");
            }

            File.WriteAllText(fullPath, downloadedResult, Encoding.UTF8);
            return fullPath;
        }

        private void FileNameCheck (ref string stringToCheck)
        {
            stringToCheck = stringToCheck.Replace(':', ' ');
        }

        private string DirectoryCheck(string folderName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, folderName);

            bool exists = Directory.Exists(fullpath);

            if (!exists)
                Directory.CreateDirectory(folderName);

            return fullpath;
        }
    }
}
