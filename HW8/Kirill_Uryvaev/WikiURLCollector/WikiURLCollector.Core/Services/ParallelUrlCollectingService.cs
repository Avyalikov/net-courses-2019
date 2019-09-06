using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;

namespace WikiURLCollector.Core.Services
{
    public class ParallelUrlCollectingService : IParallelUrlCollectingService
    {
        private readonly IPageDownloadingService pageDownloadingService;
        private readonly IUrlParsingService urlParsingService;

        private string baseAddress = "https://en.wikipedia.org";
        private object locker = new object();
        private ConcurrentDictionary<string, int> urlsDictionary;
        private List<Task> tasks;

        public ParallelUrlCollectingService(IPageDownloadingService pageDownloadingService, IUrlParsingService urlParsingService)
        {
            this.pageDownloadingService = pageDownloadingService;
            this.urlParsingService = urlParsingService;
            urlsDictionary = new ConcurrentDictionary<string, int>();
            tasks = new List<Task>();
        }

        public async Task<Dictionary<string, int>> GetUrls(string address, int maxIteration)
        {
            bool isCompleted = false;
            tasks.Add(Task.Run(() => reccyrentUrlsExtracting(address, 1, maxIteration)));
            while (!isCompleted)
            {
                await Task.WhenAll(tasks.ToArray());
                lock (locker)
                {
                    tasks.RemoveAll(t => t.IsCompleted);
                    isCompleted = tasks.Count == 0;
                }
            }
            return urlsDictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        private async Task reccyrentUrlsExtracting(string address, int iteration, int maxIteration)
        {
            var page = await pageDownloadingService.GetPage(address);
            var result = urlParsingService.ExtractAllUrlsFromPage(page, iteration);
            if (iteration <= maxIteration)
            {
                foreach (var url in result)
                {
                    if (!urlsDictionary.ContainsKey(url.URL))
                    {
                        if (urlsDictionary.TryAdd(url.URL, iteration))
                        {
                            int nextIteration = iteration + 1;
                            if (nextIteration > maxIteration)
                            {
                                continue;
                            }
                            lock (locker)
                            {
                                tasks.Add(Task.Run(() => reccyrentUrlsExtracting(baseAddress + url.URL, nextIteration, maxIteration)));
                            }
                        }
                    }
                }
            }
        }
    }
}
