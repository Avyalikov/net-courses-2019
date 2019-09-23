namespace SiteParser.Core.Services
{
    using SiteParser.Core.Repositories;

    public class DownloadPageService
    {
        private readonly IDownloader downloader;

        public DownloadPageService(IDownloader downloader)
        {
            this.downloader = downloader;
        }

        /// <summary>
        /// Downloads page from the internet and saves info local file.
        /// </summary>
        /// <param name="requestUrl">Url to download.</param>
        /// <returns></returns>
        public string DownLoadPage(string requestUrl)
        {
            var downloadedResult = this.downloader.Download(requestUrl);
            if (downloadedResult != null)
            {
                var resultPath = this.downloader.SaveIntoFile(downloadedResult);
                return resultPath;
            }

            return null;
        }
    }
}
