using SiteParser.Core.Repositories;
using SiteParser.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SiteParser.Core.Services
{
    public class UrlCollectorService
    {
        private readonly SaveIntoDatabaseService saveIntoDatabaseService;
        private readonly ParsePageService parsePageService;
        private readonly DownloadPageService downloadPageService;
        private int iterationID;

        public UrlCollectorService(ISaver saver, IDownloader downloader)
        {
            this.saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            this.parsePageService = new ParsePageService(saveIntoDatabaseService);
            this.downloadPageService = new DownloadPageService(downloader);
            this.iterationID = 0;
        }

        public string IterationCall(string pathToFile, string baseUrl)
        {
            iterationID++;
            ICollection<string> parsedUrls = parsePageService.Parse(pathToFile, baseUrl, iterationID);
            if(parsedUrls.Count == 0)
            {
                return string.Empty;
            }
            var parsedUrlsCopy = new List<string>(parsedUrls);
            ParallelLoopResult result = Parallel.ForEach<string>(parsedUrlsCopy, (item) =>
            {
                string newPath = downloadPageService.DownLoadPage(item);
                IterationCall(newPath, item);
            });
            return "IterationCall() done!";
        }

        public void InitialDowload(string fullUrl, string baseUrl)
        {
            string path = downloadPageService.DownLoadPage(fullUrl);
            IterationCall(path, baseUrl);
        }
    }
}
