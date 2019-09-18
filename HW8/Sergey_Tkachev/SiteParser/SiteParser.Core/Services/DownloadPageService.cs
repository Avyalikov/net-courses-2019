using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SiteParser.Core.Services
{
    public class DownloadPageService
    {
        private readonly IDownloader downloader;

        public DownloadPageService(IDownloader downloader)
        {
            this.downloader = downloader;
        }

        public string DownLoadPage(string requestUrl)
        {
            var downloadedResult = downloader.Download(requestUrl);
            if (downloadedResult != null)
            {
                var resultPath = downloader.SaveIntoFile(downloadedResult);
                return resultPath;
            }
            return null;
        }
    }
}
