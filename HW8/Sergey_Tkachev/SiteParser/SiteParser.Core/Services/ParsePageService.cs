namespace SiteParser.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using HtmlAgilityPack;

    public class ParsePageService
    {
        private static object locker = new object();
        private readonly SaveIntoDatabaseService saveIntoDatabaseService;
        private readonly DeleteFileService deleteFileService;
        private List<string> urls;

        public ParsePageService(SaveIntoDatabaseService saveIntoDatabaseService, DeleteFileService deleteFileService)
        {
            this.saveIntoDatabaseService = saveIntoDatabaseService;
            this.deleteFileService = deleteFileService;
            this.urls = new List<string>();
        }

        /// <summary>
        /// Returns collection of wiki urls.
        /// </summary>
        /// <param name="path">Fill path to file.</param>
        /// <param name="baseUrl">Base url of parsed page.</param>
        /// <param name="iterationID">Number of iteration. </param>
        /// <returns></returns>
        public ICollection<string> Parse(string path, string baseUrl, int iterationID)
        {
            this.urls.Clear();

            HtmlDocument htmlDocument = this.LoadFromFile(path);

            if (htmlDocument.DocumentNode.SelectNodes("//a[@href]") == null)
            {
                return this.urls;
            }

            foreach (HtmlNode link in htmlDocument.DocumentNode.SelectNodes("//a[@href]"))
            {
                string parsedUrl = link.GetAttributeValue("href", string.Empty);
                if (parsedUrl.Contains("wiki"))
                {
                    if (!parsedUrl.Contains("http"))
                    {
                        if (this.CheckIfUrlExist(baseUrl + parsedUrl))
                        {
                            break;
                        }

                        this.urls.Add(baseUrl + parsedUrl);
                        continue;
                    }

                    if (this.CheckIfUrlExist(baseUrl + parsedUrl))
                    {
                        break;
                    }

                    this.urls.Add(parsedUrl);
                }
            }

            if (this.urls.Count != 0)
            {
                lock (locker)
                {
                    this.saveIntoDatabaseService.SaveUrls(this.urls, iterationID);
                    this.deleteFileService.DeleteFile(path);
                }
            }

            return this.urls;
        }

        /// <summary>
        /// Checks if url has already contained in Database.
        /// </summary>
        /// <param name="urlToCheck">Url to check.</param>
        /// <returns></returns>
        private bool CheckIfUrlExist(string urlToCheck)
        {
            if (this.urls.Contains(urlToCheck))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads html document from a file.
        /// </summary>
        /// <param name="path">Full path to file.</param>
        /// <returns></returns>
        private HtmlDocument LoadFromFile(string path)
        {
            var htmlFile = new HtmlDocument();
            try
            {
                htmlFile.Load(path);
            }
            catch (Exception ex) when(ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                Console.WriteLine(ex.Message);
            }

            return htmlFile;
        }
    }
}
