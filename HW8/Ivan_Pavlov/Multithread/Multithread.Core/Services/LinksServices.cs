namespace Multithread.Core.Services
{
    using HtmlAgilityPack;
    using Multithread.Core.Models;
    using Multithread.Core.Repo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public  class LinksServices
    {
        private string startUrl;
        private static int Iteration = 0;
        private readonly ILinksRepo repo;
        private readonly Random rnd = new Random();

        public LinksServices(ILinksRepo repo)
        {
            this.repo = repo;
        }

        public HtmlDocument DowloandPage(string url)
        {
            if (this.startUrl == null)
            {
                this.startUrl = url.Split(new string[] { "/wiki/" },
                    StringSplitOptions.RemoveEmptyEntries)[0];
                SaveTagsIntoDb(new List<string>() { url });
                Iteration++;
            }

            //WebClient wc = new WebClient();
            //wc.Proxy = new WebProxy("159.89.83.24:8080");
            //var page = wc.DownloadString(url);

            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(page);
            //return doc;

            var web = new HtmlWeb();
            Thread.Sleep(rnd.Next(20, 100));
            return web.Load(url);
        }       

        public ICollection<string> ExtraxctHtmlTags(string url)
        {
            var page = DowloandPage(url);

            var pagesLinks = page.DocumentNode
                                 .Descendants("a")
                                 .Select(a => a.GetAttributeValue("href", null))
                                 .Where(u => !String.IsNullOrEmpty(u));
            page = null;

            ICollection<string> urls = new List<string>();
            foreach (var link in pagesLinks)
            {
#if DEBUG
                if (new List<int> { 1, 2, 3}.Contains(rnd.Next(1, 10)))
                    break;
#endif
                if (link.Contains(startUrl))
                    urls.Add(link);

                if (link.StartsWith("/wiki/"))
                    urls.Add(startUrl + link);
            }
            return urls.Distinct().ToList();
        }

        public void SaveTagsIntoDb(ICollection<string> urls)
        {
            foreach(var ur in urls)
            {
                if (!this.repo.Contains(ur))
                {
                    this.repo.CheckAddSave(new Link()
                    {
                        Url = ur,
                        IterationId = Iteration
                    });
                }             
            }
        }

        public void ParsingForEachPage(int endIteration)
        {
            if (Iteration == endIteration)
            {
                return;
            }          

            var urls = this.repo.GetAllWithIteration(Iteration);

            if (urls.Count != urls.Distinct().ToList().Count)
            {
                this.repo.RemoveDuplicate();
#if DEBUG
                    Console.WriteLine("все таки они умудряются повторятся");
#endif
            }
            Iteration++;
            ParallelLoopResult result = Parallel.ForEach<string>(urls, SingleThread);            

            ParsingForEachPage(endIteration);
        }

        public void SingleThread(string url)
        {
            Thread.Sleep(10);
            var urls = ExtraxctHtmlTags(url).Distinct().ToList();
            SaveTagsIntoDb(urls);
        }
    }
}
 