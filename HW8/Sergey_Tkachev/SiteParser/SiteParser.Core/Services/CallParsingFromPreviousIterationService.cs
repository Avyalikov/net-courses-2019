using SiteParser.Core.Repositories;
using SiteParser.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiteParser.Core.Services
{
    public class CallParsingFromPreviousIterationService
    {
        private readonly SaveIntoDatabaseService saveIntoDatabaseService;
        private readonly ParsePageService parsePageService;
        private readonly DownloadPageService downloadPageService;

        public CallParsingFromPreviousIterationService(ISaver saver, IDownloader downloader)
        {
            this.saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            this.parsePageService = new ParsePageService(saveIntoDatabaseService);
            this.downloadPageService = new DownloadPageService(downloader);
        }

        public string IterationCall(string path, string url)
        {
            ICollection<string> parsedUrls = parsePageService.Parse(path, url);
            if(parsedUrls.Count == 0)
            {
                return string.Empty;
            }
            var parsedUrlsCopy = new List<string>(parsedUrls);
            foreach (var item in parsedUrlsCopy)
            {
                string newPath = downloadPageService.DownLoadPage(item);
                IterationCall(newPath, item);
            }
            return "IterationCall() done!";
        }
    }
}
