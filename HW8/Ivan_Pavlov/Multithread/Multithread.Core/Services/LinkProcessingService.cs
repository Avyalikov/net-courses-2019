namespace Multithread.Core.Services
{
    using HtmlAgilityPack;
    using Multithread.Core.Models;
    using Multithread.Core.Repo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class LinkProcessingService
    {
        private string startUrl;
        private int Iteration = 0;
        private readonly ILinksRepo linkRepo;
        private readonly IWebRepo webRepo;
        private readonly Random rnd = new Random();

        public LinkProcessingService(ILinksRepo linkRepo, IWebRepo webRepo)
        {
            this.linkRepo = linkRepo;
            this.webRepo = webRepo;
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

            return webRepo.DowloandPage(url);
        }

        public ICollection<string> ExtraxctHtmlTags(string url)
        {
            var page = DowloandPage(url);

            var pagesLinks = webRepo.GetAllTagsFromPage(page);
            page = null;

            ICollection<string> urls = new List<string>();
            foreach (var link in pagesLinks)
            {
                if (link.Contains(startUrl))
                    urls.Add(link);

                if (link.StartsWith("/wiki/"))
                    urls.Add(startUrl + link);
            }
            return urls.Distinct().ToList();
        }

        public void SaveTagsIntoDb(ICollection<string> urls)
        {
            foreach (var ur in urls)
            {
                if (!this.linkRepo.Contains(ur))
                {
                    this.linkRepo.CheckAddSave(new Link()
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

            var urls = this.linkRepo.GetAllWithIteration(Iteration);

            if (urls.Count != urls.Distinct().ToList().Count)
            {
                this.linkRepo.RemoveDuplicate();
#if DEBUG
                Console.WriteLine("есть дубликаты");
                if (this.linkRepo.GetAllWithIteration(Iteration).Count == urls.Distinct().ToList().Count)
                    Console.WriteLine("костыль отработал, дубликатов больше нет");
#endif
            }
            Iteration++;
     
            Parallel.ForEach<string>(urls, url =>
            {
                {
                    Task t = Task.Factory.StartNew(() => { SingleThread(url); });
                    t.Wait();
                }
            });

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
