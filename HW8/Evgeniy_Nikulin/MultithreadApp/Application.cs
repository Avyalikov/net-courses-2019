namespace MultithreadApp
{
    using MultithreadApp.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public class Application : IApplication
    {
        private readonly ILinksServices linksServices;
        private readonly IHttpProvider wiki;

        public Application(
            ILinksServices linksServices,
            IHttpProvider wiki)
        {
            this.linksServices = linksServices;
            this.wiki = wiki;
        }

        public async void Run()
        {
            List<string> pages = new List<string>();

            string page;

            while (true)
            {
                Console.WriteLine("Enter start page:");
                Console.Write(wiki.BaseUrl);
                page = Console.ReadLine();

                if (wiki.IsExist(page))
                {
                    break;
                }

                Console.WriteLine("This page doesn't exist, try again" + Environment.NewLine);
            }

            pages.Add(page);

            DoParseAsync(pages, 1, 3).Wait();

            Console.ReadKey();
        }

        private async Task DoParseAsync(List<string> pages, int iteration, int maxIter)
        {
            Console.WriteLine($"Start {iteration} iteration, first page - {pages[0]}");

            pages = linksServices.SavePagesToDb(pages, iteration);

            await linksServices.DownloadPages(pages);

            if (iteration == maxIter)
            {
                return;
            }

            var pagesList = linksServices.ParseWikiPages(pages);

            var taskList = new List<Task>();
            foreach (var pl in pagesList)
            {
                taskList.Add(Task.Factory.StartNew(() => DoParseAsync(pl, iteration + 1, maxIter)));
            }

            Task.WaitAll(taskList.ToArray());
        }        
    }
}