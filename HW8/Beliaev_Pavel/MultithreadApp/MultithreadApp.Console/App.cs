using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using MultithreadApp.Console.UnitsOfWork;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace MultithreadApp.Console
{
    class App
    {
        private readonly DownloadService downloadService;
        private readonly ParsingService parsingService;
        //private object locker = new object();
        public static Mutex mutexObj;

        public App(DownloadService downloadService, ParsingService parsingService)
        {
            this.downloadService = downloadService;
            this.parsingService = parsingService;
            Directory.CreateDirectory("Pages");

            if (!Mutex.TryOpenExisting("MultithreadAppMutex", out mutexObj))
            {
                mutexObj = new Mutex(false, "MultithreadAppMutex");
            }
        }
                     
        public async Task Run(string url, int depth)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                UrlService urlService = new UrlService(unitOfWork);
                await Task.Run(() =>
                {
                    Thread.Sleep(500);
                    
                    if (depth > 0)
                    {
                        int iteration = urlService.GetCurrentIteration();
                        string filename =  $"Pages\\{url.GetHashCode()}.html";//Pages\\
                        this.downloadService.DownloadHtml(url, filename);
                        List<string> linksToAdd = this.parsingService.GetLinksFromHtml(filename, url);
                        if (linksToAdd != null)
                        {
                            //lock (locker)
                            //{
                            mutexObj.WaitOne();
                            urlService.AddParsedLinksToDB(linksToAdd, iteration);
                            mutexObj.ReleaseMutex();
                        //}
                    }

                        if (depth > 1)
                        {
                            var linksFromPreviousIteration = urlService.GetAllLinksByIteration(iteration).ToList();
                            if (linksFromPreviousIteration.Count > 0)
                            {
                                Parallel.ForEach<string>(linksFromPreviousIteration, (link) =>
                                {
                                    Task t = Run(link, depth - 1);
                                    t.Wait();
                                });
                            }
                        }
                    }
                });
            }
        }
        ~App()
        {
            mutexObj.Dispose();
        }
    }
}
