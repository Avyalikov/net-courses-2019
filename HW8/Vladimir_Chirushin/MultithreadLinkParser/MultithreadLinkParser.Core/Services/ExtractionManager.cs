namespace MultithreadLinkParser.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class ExtractionManager : IExtractionManager
    {
        const int maxRecursionDepth = 4;

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
            if (client != null)
            {
                client.Dispose();
            }
        }

        public async Task<bool> MyRecAsync(string urlToParse, int linkLayer, CancellationToken cts)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            var fileName = pageDownloader.DownloadPage(urlToParse, client, cts);
            List<string> newUrls = new List<string>();

            using (StreamReader sw = new StreamReader(await fileName))
            {
                string line = sw.ReadLine();
                while (line != null)
                {
                    newUrls.AddRange(htmlTagExtractor.ExtractTags(line, urlToParse));
                    line = sw.ReadLine();
                }
            }
            Console.WriteLine($"There is {newUrls.Count} extracted from layer {linkLayer}");

            await tagsDataBaseManager.AddLinksAsync(newUrls, linkLayer, cts);

            if (linkLayer < maxRecursionDepth)
            {
                IEnumerable<Task<bool>> downloadTasksQuery =
                    from url in newUrls select MyRecAsync(url, linkLayer + 1, cts);

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
