using Multithread.Core.Dto;
using Multithread.Core.Services;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadConsoleApp
{
    public class Multithread
    {
        private static int num = 0;
        private static int substr = 0;

        private readonly IHtmlReader htmlReader;
        private readonly IDataBaseManager dataBaseManager;
        private readonly IHtmlParser htmlParser;
        private readonly IFileSystemManager fileSystemManager;
        private readonly object locker;
        private List<Task> tasks;


        public Multithread(IHtmlParser htmlParser, IHtmlReader htmlReader, IFileSystemManager fileSystemManager, IDataBaseManager dataBaseManager)
        {
            this.htmlParser = htmlParser;
            this.htmlReader = htmlReader;
            this.fileSystemManager = fileSystemManager;
            this.dataBaseManager = dataBaseManager;
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
            
            tasks.Add(Task.Run(() => GetUrlsUsingRecursion(url, startIteration, maxIteration)));
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
        
        private string GenerateFileName()
        {
            substr++;
            return "C:\\multi\\file" + substr.ToString() + ".txt";
        }

        //private async Task<List<string>> ParseHtml(string url)
        //{
        //    string result;
        //    List<string> collection = new List<string>();
        //    try
        //    {
        //        result = await this.htmlReader.ReadHttp(url);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    if (string.IsNullOrEmpty(result))
        //        return null;
        //    await this.SaveDeleteFile(result);
        //    collection = this.htmlParser.FindLinksFromHtml(result);
        //    return collection; 
        //}
        private List<string> ParseHtml(string html)
        {
           return this.htmlParser.FindLinksFromHtml(html);
        }

        private async Task<string> LoadHtml(string url)
        {
            string result;
            try
            {
                result = await this.htmlReader.ReadHttp(url);
            }
            catch (Exception)
            {
                return null;
            }

            if (string.IsNullOrEmpty(result))
                return null;
            return result;
        }

        private async Task SaveDeleteFile(string result)
        {
            var filename = this.GenerateFileName();
            await fileSystemManager.WriteToFile(result, filename);
            fileSystemManager.DeleteFile(filename);
        }

        private void AddCollectionToDB(List<string> collection, int iteration)
        {
            lock (locker)
            {
                this.dataBaseManager.AddLinksToDB(collection, iteration);
            }
        }

        private async Task GetUrlsUsingRecursion(string url, int iteration, int maxIteration)
        {
            Thread.Sleep(10);
            if (iteration > maxIteration)
            {
                return;
            }
            num++;
            Console.WriteLine($"Start {iteration} iteration with number {num}");

            var result = await LoadHtml(url);
            if (result == null)
                return;

            await this.SaveDeleteFile(result);

            var collection = this.ParseHtml(result);
            if (collection.Count == 0)
                return;

            this.AddCollectionToDB(collection, iteration);
            foreach (var item in collection)
            {
                tasks.Add(Task.Run(() => GetUrlsUsingRecursion(item, iteration + 1, maxIteration)));
            }
        }
    }
}