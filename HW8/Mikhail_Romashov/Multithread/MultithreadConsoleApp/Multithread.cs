using Multithread.Core.Dto;
using Multithread.Core.Services;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadConsoleApp
{
    class Multithread
    {
        private static int num = 0;
        private static int substr = 0;

        private readonly LinkService linkService;
        private readonly IHtmlParser htmlParser;
        private readonly object locker;
        private List<Task> tasks;


        public Multithread(LinkService linkService, IHtmlParser htmlParser)
        {
            this.htmlParser = htmlParser;
            this.linkService = linkService;
            locker = new object();
            tasks = new List<Task>();
        }

        public async Task Run()
        {
            string url = UrlForWork.GetUrl();
            if (url == null)
                return;
            bool isCompleted = false;
            int startIteration = 1;
            int maxIteration = 2;
            
            tasks.Add(Task.Run(() => StartWithRecursion(url, startIteration, maxIteration)));
            while (!isCompleted)
            {
                await Task.WhenAll(tasks.ToArray());
                lock (locker)
                {
                    tasks.RemoveAll(t => t.IsCompleted);
                    isCompleted = tasks.Count == 0;
                }
            }
        }
        
        private string GenerateFileName(string url)
        {
            substr++;
            return "C:\\multi\\file" + substr.ToString() + ".txt";
        }

        private async Task<List<string>> ParseHtml(string url)
        {
            string result;
            List<string> collection = new List<string>();
            try
            {
                result = await HtmlReader.ReadHttp(url);
            }
            catch (Exception)
            {
                return null;
            }

            if (string.IsNullOrEmpty(result))
                return null;
            //var filename = this.GenerateFileName(url);
            //IOFile.WriteToFile(result.Result, filename);
            //var newResult = IOFile.ReadFromFile(filename);
            //IOFile.DeleteFile(filename);
            collection = this.htmlParser.FindLinksFromHtml(result);
            return collection; 
        }

        private void AddCollectionToDB(List<string> collection, int iteration)
        {
            
            lock (locker)
            {
                foreach (var item in collection)
                {
                    try
                    {
                        var id = linkService.AddNewLink(new LinkInfo()
                        {
                            Link = item,
                            Iteration = iteration
                        });
                    }
                    catch (ArgumentException)
                    {
                         continue;
                    }
                }
            }
        }

        private async Task StartWithRecursion(string url, int iteration, int maxIteration)
        {
            Thread.Sleep(10);
            if (iteration > maxIteration)
            {
                return;
            }

            num++;
            Console.WriteLine($"Start {iteration} iteration with number {num}");
            var collection = await ParseHtml(url);
            if (collection.Count == 0)
                return;
            this.AddCollectionToDB(collection, iteration);
            foreach (var item in collection)
            {
                tasks.Add(Task.Run(() => StartWithRecursion(item, iteration + 1, maxIteration)));
            }
        }
    }
}