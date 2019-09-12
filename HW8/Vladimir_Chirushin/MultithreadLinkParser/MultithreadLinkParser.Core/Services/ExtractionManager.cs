namespace MultithreadLinkParser.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class ExtractionManager : IExtractionManager
    {
        private const int MaxRecursionDepth = 4;

        private readonly IPageDownloaderService pageDownloader;
        private readonly IHtmlTagExtractorService htmlTagExtractor;
        private readonly ITagsDataBaseManager tagsDataBaseManager;

        private HttpClient client = new HttpClient();

        public ExtractionManager(IHtmlTagExtractorService htmlTagExtractor, IPageDownloaderService pageDownloader, ITagsDataBaseManager tagsDataBaseManager)
        {
            this.pageDownloader = pageDownloader;
            this.htmlTagExtractor = htmlTagExtractor;
            this.tagsDataBaseManager = tagsDataBaseManager;
        }

        ~ExtractionManager()
        {
            if (this.client != null)
            {
                this.client.Dispose();
            }
        }

        public async Task<bool> RecursionTagExtraction(string urlToParse, int linkLayer, CancellationToken cts)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            var fileName = this.pageDownloader.DownloadPage(urlToParse, this.client, cts);
            List<string> newUrls = new List<string>();

            using (StreamReader sw = new StreamReader(await fileName))
            {
                string line = sw.ReadLine();
                while (line != null)
                {
                    newUrls.AddRange(this.htmlTagExtractor.ExtractTags(line, urlToParse));
                    line = sw.ReadLine();
                }
            }
            Console.WriteLine($"There is {newUrls.Count} extracted from layer {linkLayer}");

            this.tagsDataBaseManager.AddLinksAsync(newUrls, linkLayer, cts);

            if (linkLayer < MaxRecursionDepth)
            {
                IEnumerable<Task<bool>> downloadTasksQuery =
                    from url in newUrls select this.RecursionTagExtraction(url, linkLayer + 1, cts);

                List<Task<bool>> downloadTasks = downloadTasksQuery.ToList();

                while (downloadTasks.Count > 0)
                {
                    Task<bool> firstFinishedTask = await Task.WhenAny(downloadTasks);

                    downloadTasks.Remove(firstFinishedTask);
                }
            }
            Console.WriteLine($"Recursion level {linkLayer} closed.");
            return true;
        }
    }
}
