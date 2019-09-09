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
            var resultPath = downloader.SaveIntoFile(downloadedResult);
            return resultPath;
            //using (HttpClient client = new HttpClient())
            //{
            //    using (HttpResponseMessage response = client.GetAsync(requestUrl).Result)
            //    {
            //        using (HttpContent content = response.Content)
            //        {
            //            string result = content.ReadAsStringAsync().Result;
            //            return result; 
            //        }
            //    }
            //}
        }
    }
}
