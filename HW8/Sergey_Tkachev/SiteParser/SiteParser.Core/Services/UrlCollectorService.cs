namespace SiteParser.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SiteParser.Core.Repositories;

    public class UrlCollectorService
    {
        private readonly SaveIntoDatabaseService saveIntoDatabaseService;
        private readonly ParsePageService parsePageService;
        private readonly DownloadPageService downloadPageService;
        private readonly DeleteFileService deleteFileService;
        private int multyIterationID;
        private int iterationID;

        public UrlCollectorService(ISaver saver, IDownloader downloader, ICleaner cleaner)
        {
            this.saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            this.deleteFileService = new DeleteFileService(cleaner);
            this.parsePageService = new ParsePageService(this.saveIntoDatabaseService, this.deleteFileService);
            this.downloadPageService = new DownloadPageService(downloader);
            this.multyIterationID = 0;
            this.iterationID = 0;
        }

        /// <summary>
        /// Runs the solothread way.
        /// </summary>
        /// <param name="startPageToParse">Initial page adress.</param>
        /// <param name="baseUrl">Base url of site.</param>
        /// <returns></returns>
        public async Task<string> Solothread(string startPageToParse, string baseUrl)
        {
            string initialPath = this.downloadPageService.DownLoadPage(startPageToParse);
            string result = await Task.Run(() => this.IterationCall(initialPath, baseUrl));
            return result;
        }

        /// <summary>
        /// Runs the multithread way.
        /// </summary>
        /// <param name="startPageToParse">Initial page adress.</param>
        /// <param name="baseUrl">Base url of site.</param>
        /// <returns></returns>
        public async Task<string> Multithread(string startPageToParse, string baseUrl)
        {
            string initialPath = this.downloadPageService.DownLoadPage(startPageToParse);
            string result = await Task.Run(() => this.ParalellIterationCall(initialPath, baseUrl));
            return result;
        }

        /// <summary>
        /// Recursive method for running download, parse and save url with paralell execution.
        /// </summary>
        /// <param name="pathToFile">Path to file for parsing.</param>
        /// <param name="baseUrl">Base url of parsed page.</param>
        /// <returns></returns>
        private string ParalellIterationCall(string pathToFile, string baseUrl)
        {
            this.multyIterationID++;
            ICollection<string> parsedUrls = this.parsePageService.Parse(pathToFile, baseUrl, this.multyIterationID);
            if (parsedUrls.Count == 0)
            {
                return string.Empty;
            }

            var parsedUrlsCopy = new List<string>(parsedUrls);
            ParallelLoopResult result = Parallel.ForEach<string>(
                parsedUrlsCopy, 
                (item) =>
                {
                    string newPath = downloadPageService.DownLoadPage(item);
                    if (newPath != null)
                    {
                        ParalellIterationCall(newPath, item);
                    }
                });
            return "ParalellIterationCall() done!";
        }

        /// <summary>
        /// Recursive method for running download, parse and save url.
        /// </summary>
        /// <param name="pathToFile">Path to file for parsing.</param>
        /// <param name="baseUrl">Base url of parsed page.</param>
        /// <returns></returns>
        private string IterationCall(string pathToFile, string baseUrl)
        {
            this.iterationID++;
            ICollection<string> parsedUrls = this.parsePageService.Parse(pathToFile, baseUrl, this.iterationID);
            if (parsedUrls.Count == 0)
            {
                return string.Empty;
            }

            var parsedUrlsCopy = new List<string>(parsedUrls);
            foreach (string item in parsedUrlsCopy)
            {
                string newPath = this.downloadPageService.DownLoadPage(item);
                if (newPath != null)
                {
                    this.IterationCall(newPath, item);
                }
            }

            return "IterationCall() done!";
        }
    }
}
