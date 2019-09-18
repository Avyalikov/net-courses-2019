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
        private readonly DeleteFileService deleteFileService;
        private int multyIterationID;
        private int iterationID;

        public UrlCollectorService(ISaver saver, IDownloader downloader, ICleaner cleaner)
        {
            this.saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            this.deleteFileService = new DeleteFileService(cleaner);
            this.parsePageService = new ParsePageService(saveIntoDatabaseService, deleteFileService);
            this.downloadPageService = new DownloadPageService(downloader);
            this.multyIterationID = 0;
            this.iterationID = 0;
        }

        private string ParalellIterationCall(string pathToFile, string baseUrl)
        {
            multyIterationID++;
            ICollection<string> parsedUrls = parsePageService.Parse(pathToFile, baseUrl, multyIterationID);
            if(parsedUrls.Count == 0)
            {
                return string.Empty;
            }
            var parsedUrlsCopy = new List<string>(parsedUrls);
            ParallelLoopResult result = Parallel.ForEach<string>(parsedUrlsCopy, (item) =>
            {
                string newPath = downloadPageService.DownLoadPage(item);
                if(newPath != null)
                {
                    ParalellIterationCall(newPath, item);
                }
            });
            return "ParalellIterationCall() done!";
        }

        private string IterationCall(string pathToFile, string baseUrl)
        {
            iterationID++;
            ICollection<string> parsedUrls = parsePageService.Parse(pathToFile, baseUrl, iterationID);
            if (parsedUrls.Count == 0)
            {
                return string.Empty;
            }
            var parsedUrlsCopy = new List<string>(parsedUrls);
            foreach (string item in parsedUrlsCopy)
            {
                string newPath = downloadPageService.DownLoadPage(item);
                if (newPath != null)
                {
                    IterationCall(newPath, item);
                }
            }
            return "IterationCall() done!";
        }

        public async Task<string> Solothread(string startPageToParse, string baseUrl)
        {
            string initialPath = downloadPageService.DownLoadPage(startPageToParse);
            string result = await Task.Run(()=> IterationCall(initialPath, baseUrl));
            return result;
        }

        public async Task<string> Multithread(string startPageToParse, string baseUrl)
        {
            string initialPath = downloadPageService.DownLoadPage(startPageToParse);
            string result = await Task.Run(() => ParalellIterationCall(initialPath, baseUrl));
            return result;
        }
       
    }
}
