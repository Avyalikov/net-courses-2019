using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;
using WikiURLCollector.Core.Models;
using WikiURLCollector.Core.Services;

namespace WikiURLCollector.ConsoleApp
{
    public class ParallelUrlCollector
    {
        private readonly UrlParsingService parsingService;
        private readonly IUrlService urlService;
        private readonly PageDownloadingService pageDownloadingService;

        string baseAdress = "https://en.wikipedia.org/";
        object locker = new object();
        ConcurrentDictionary<string, int> urlsDictionary;
        List<Task> tasks;

        public ParallelUrlCollector(UrlParsingService parsingService, IUrlService urlService, PageDownloadingService pageDownloadingService)
        {
            this.parsingService = parsingService;
            this.urlService = urlService;
            this.pageDownloadingService = pageDownloadingService;
            urlsDictionary = new ConcurrentDictionary<string, int>();
            tasks = new List<Task>();
        }

        public async Task<Dictionary<string, int>> GetUrls(string userInput, int maxIterations)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine($"{DateTime.Now} Start url parsing");
            watch.Start();
            tasks.Add(loadPage(userInput, 1, maxIterations));
            await waitTasksComplition();
            watch.Stop();
            Console.WriteLine($"Parsing with derph {maxIterations} is completed in {watch.Elapsed}");
            return urlsDictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        private async Task waitTasksComplition()
        {
            bool isCompleted = false;
            while (!isCompleted)
            {
                await Task.WhenAll(tasks.ToArray());
                lock (locker)
                {
                    tasks.RemoveAll(t => t.IsCompleted);
                    isCompleted = tasks.Count == 0;
                    Console.WriteLine($"Remained tasks: {tasks.Count} collected urls: {urlsDictionary.Count}");
                }
            }
        }

        private async Task loadPage(string adress, int iteration, int maxIteration)
        {
            var page = await pageDownloadingService.GetPage(adress);
            var result = parsingService.ExtractAllUrlsFromPage(page, iteration);
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
                                tasks.Add(Task.Run(() => loadPage(baseAdress + url.URL, nextIteration, maxIteration)));
                            }
                        }
                    }
                }                
            }
        }
    }
}
