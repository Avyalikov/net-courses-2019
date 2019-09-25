namespace MultithreadLinksApp.Core.Services
{
    using Interfaces;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class URLCollectingService : IURLCollectingService
    {
        private readonly IPageService pageService;
        private readonly IURLParsingService urlParsingService;

        private readonly string baseURL = "https://en.wikipedia.org";
        private object locker = new object();
        private Dictionary<string, int> urlsDict;
        private List<Task> tasks;

        public URLCollectingService(IPageService pageService, IURLParsingService urlParsingService)
        {
            this.pageService = pageService;
            this.urlParsingService = urlParsingService;
            urlsDict = new Dictionary<string, int>();
            tasks = new List<Task>();
        }

        public async Task<Dictionary<string, int>> CollectURLs(string url, int recursionDepth)
        {
            bool isCompleted = false;
            tasks.Add(Task.Run(() => ExtractURLs(url, 1, recursionDepth)));
            while (!isCompleted)
            {
                await Task.WhenAll(tasks);
                lock (locker)
                {
                    tasks.RemoveAll(t => t.IsCompleted);
                    if (tasks.Count() == 0)
                    {
                        isCompleted = true;
                    }
                }
            }
            return urlsDict;
        }

        private async Task ExtractURLs(string url, int currentDepth, int recursionDepth)
        {
            var pageFile = await pageService.DownloadPage(url);
            var page = pageService.ReadPageFromFile(pageFile);
            var allURLs = urlParsingService.GetAllURLsFromPage(page, currentDepth);
            if (currentDepth <= recursionDepth)
            {
                foreach (var address in allURLs)
                {
                    if (!urlsDict.ContainsKey(address.Url))
                    {
                        try 
                        {
                            urlsDict.Add(address.Url, currentDepth);
                            int nextIteration = currentDepth + 1;
                            if (nextIteration > recursionDepth)
                            {
                                continue;
                            }

                            lock (locker)
                            {
                                tasks.Add(Task.Run(() => ExtractURLs(baseURL + address.Url, nextIteration, recursionDepth)));
                            }
                        }

                        catch (Exception)
                        {
                            throw new Exception();
                        }
                    }
                }
            }
        }
    }
}
