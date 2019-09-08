namespace MultithreadApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using DataBase;
    using DataBase.Model;
    using Interfaces;

    public class Application : IApplication
    {
        private readonly object dbLock = new object();

        private readonly IDataBase db;
        private readonly IFileProvider file;
        private readonly IHttpProvider wiki;

        public Application(
            IDataBase db,
            IFileProvider file,
            IHttpProvider wiki)
        {
            this.db = db;
            this.file = file;
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

            MainAsync(pages, 1, 3).Wait();

            Console.ReadKey();
        }

        public async Task MainAsync(List<string> pages, int iteration, int maxIter)
        {
            Console.WriteLine($"Start {iteration} iteration, first page - {pages[0]}");

            pages = SavePagesToDb(pages, iteration);

            await DownloadPages(pages);

            if (iteration == maxIter)
            {
                return;
            }

            var pagesList = ParseWikiPages(pages);

            var taskList = new List<Task>();
            foreach (var pl in pagesList)
            {
                taskList.Add(Task.Factory.StartNew(() => MainAsync(pl, iteration + 1, maxIter)));
            }

            Task.WaitAll(taskList.ToArray());
        }

        public async Task DownloadPage(string page)
        {
            Console.WriteLine($"\tDownload {page}");

            string response = await wiki.GetHtmlAsync(page);
            await file.SaveToFileAsync(page, response);
        }
        public async Task DownloadPages(IEnumerable<string> pages)
        {
            foreach (var page in pages)
            {
                await DownloadPage(page);
            }
        }

        public List<string> SavePagesToDb(List<string> pages, int iteration)
        {
            var pagesInDb = new List<string>();

            lock (dbLock)
            {
                db.Connect();

                foreach (var page in pages)
                {
                    string url = wiki.BaseUrl + page;

                    if (db.Links.IsExist(url))
                    {
                        continue;
                    }

                    db.Links.Add(
                        new Links()
                        {
                            link = url,
                            iterationId = iteration,
                        });

                    pagesInDb.Add(page);
                }

                db.SaceChanges();
                db.Disconnect();
            }

            return pagesInDb;
        }

        public List<string> ParseWikiPage(string page)
        {
            var links = new List<string>();

            var doc = new HtmlDocument();
            string html = file.LoadHtml(page);
            doc.LoadHtml(html);

            foreach (var node in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string link = node.Attributes["href"].Value;
                if (link.StartsWith("/wiki/") && !link.Contains(":"))
                {
                    link = link
                        .Replace("/wiki/", string.Empty)
                        .Replace("/", "//")
                        .Split('#')[0];

                    if (!links.Contains(link))
                    {
                        links.Add(link);
                    }
                }
            }

            return links;
        }
        public List<List<string>> ParseWikiPages(IEnumerable<string> pages)
        {
            var pagesList = new List<List<string>>();
            foreach (var page in pages)
            {
                pagesList.Add(ParseWikiPage(page));
            }

            return pagesList;
        }
    }
}