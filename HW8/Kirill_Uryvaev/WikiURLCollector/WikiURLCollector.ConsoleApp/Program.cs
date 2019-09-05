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
        static string baseAdress = "https://en.wikipedia.org/";
        static UrlParsingService parsingService;
        static UrlService urlService;
        static object locker = new object();
        static void Main(string[] args)
        {
            var container = new Container(new WikiUrlRegistry());
            parsingService = container.GetInstance<UrlParsingService>();
            urlService = container.GetInstance<UrlService>();
            string exitCode = "e";
            string userInput = "";
            int maxIterations = 2;
            Console.WriteLine($"{DateTime.Now} Program started");
            while (!userInput.ToLower().Equals(exitCode))
            {
                userInput = Console.ReadLine();
                GetUrls(userInput, maxIterations);
            }
        }

        static async void GetUrls(string userInput, int maxIterations)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine($"{DateTime.Now} Start url parsing");
            watch.Start();
            await LoadPage(userInput, 1, maxIterations);
            watch.Stop();
            Console.WriteLine($"Parsing with derph {maxIterations} is completed in {watch.Elapsed}");
        }

        static async Task LoadPage(string adress, int iteration, int maxIteration)
        {
            IEnumerable<UrlEntity> result = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(adress);
                if (response.IsSuccessStatusCode)
                {
                    var pageContents = await response.Content.ReadAsStringAsync();
                    result = parsingService.ExtractAllUrlsFromPage(pageContents, iteration);                    
                }
            }
            if (result==null)
            {
                return;
            }
            if (iteration <= maxIteration)
            {
                List<Task> tasks = new List<Task>();
                foreach (var url in result)
                {
                    if (urlService.GetUrl(url.URL) == null)
                    {
                        lock(locker)
                        {
                            if (urlService.GetUrl(url.URL) == null)
                            {
                                urlService.AddUrl(url);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        int nextIteration = iteration++;
                        tasks.Add(Task.Factory.StartNew(() => LoadPage(baseAdress + url.URL, nextIteration, maxIteration)));
                    }
                }
                Task.WaitAll(tasks.ToArray());
            }
        }
    }
} 
