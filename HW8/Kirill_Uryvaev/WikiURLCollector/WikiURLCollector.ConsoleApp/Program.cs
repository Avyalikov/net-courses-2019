using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Models;
using StructureMap;
using System.Diagnostics;
using System.Threading;

namespace WikiURLCollector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new WikiUrlRegistry());
            var parsingService = container.GetInstance<UrlParsingService>();
            var urlService = container.GetInstance<UrlService>();
            string exitCode = "e";
            string userInput = "";
            int maxIterations = 5;
            Console.WriteLine($"{DateTime.Now} Program started");
            while (!userInput.ToLower().Equals(exitCode))
            {
                userInput = Console.ReadLine();
                GetUrls(userInput, parsingService, urlService, maxIterations);
            }
        }

        static async void GetUrls(string userInput, UrlParsingService parsingService, UrlService urlService, int maxIterations)
        {
            List<Task<IEnumerable<UrlEntity>>> tasks = new List<Task<IEnumerable<UrlEntity>>>();
            List<Task<IEnumerable<UrlEntity>>> nextTasks = new List<Task<IEnumerable<UrlEntity>>>();
            CancellationToken token = new CancellationToken();
            int i = 1;
            tasks.Add(new Task<IEnumerable<UrlEntity>>(()=> { return LoadPage(userInput, parsingService, i).Result; }, token));
            Stopwatch watch = new Stopwatch();
            string baseAdress = "https://en.wikipedia.org/";
            Console.WriteLine($"{DateTime.Now} Start url parsing");
            while (tasks.Count > 0 && i <= maxIterations)
            {
                watch.Restart();
                tasks.ForEach(t => t.Start());
                await Task.WhenAll(tasks);
                watch.Stop();
                if (tasks.Any(t => t.Exception != null))
                {
                    Console.WriteLine("Error has occurred");
                    return;
                }
                Console.WriteLine($"Iteration {i} is completed in {watch.Elapsed}");

                i++;
                foreach (var task in tasks)
                {
                    foreach (var url in task.Result)
                    {
                        if (urlService.GetUrl(url.URL) == null)
                        {
                            urlService.AddUrl(url);
                            nextTasks.Add(new Task<IEnumerable<UrlEntity>>(() => { return LoadPage(baseAdress + url.URL, parsingService, i).Result; }, token));
                        }
                    }
                }
                tasks.Clear();
                tasks = nextTasks;
                nextTasks = new List<Task<IEnumerable<UrlEntity>>>();
            }
        }

        static async Task<IEnumerable<UrlEntity>> LoadPage(string adress, UrlParsingService parsingService, int iteration)
        {
            IEnumerable<UrlEntity> result = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(adress).Result)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var pageContents = await response.Content.ReadAsStringAsync();
                            result = parsingService.ExtractAllUrlsFromPage(pageContents, iteration);
                        }
                    }
                }
            }
            catch (AggregateException e)
            {
                Thread.Sleep(1000);
            }
            return result;
        }
    }
}
