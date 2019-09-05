namespace MultithreadApp.Core.Services
{
    using HtmlAgilityPack;
    using MultithreadApp.Core.Model;
    using MultithreadApp.Core.Repo;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinksServices
    {
        private readonly ILinksTableRepo repo;
        private static string startDomen { get; set; }
        private static HtmlDocument startPage { get; set; }

        public LinksServices(ILinksTableRepo repo)
        {
            this.repo = repo;
        }

        public HtmlDocument DownloadPage(string url)
        {
            var web = new HtmlWeb();

            if (startPage == null)
            {
                startPage = web.Load(url);
                return startPage;
            }

            return web.Load(url);
        }

        public List<string> ExtractHtmlTags(HtmlDocument page, string url)
        {
            List<string> urls = new List<string>();

            var pagesLinks = page.DocumentNode
                                  .Descendants("a")
                                  .Select(a => a.GetAttributeValue("href", null))
                                  .Where(u => !String.IsNullOrEmpty(u));
            if (pagesLinks.Count() == 0)
                throw new ArgumentException("На странице нет ссылок");

            foreach (var link in pagesLinks)
            {
                if (link.Contains(startDomen)) // ссылки только в домене базовой ссылки
                    urls.Add(link);
                if (link.StartsWith("/wiki/")) 
                {
                    urls.Add(startDomen + link);
                }
            }
            if (urls.Count() == 0)
                throw new ArgumentException("На странице нет искомых ссылок");

            return urls;
        }

        public void SaveTagsIntoDb(string url, Link fatherLink = null)
        {
            Link link = this.repo.GetByURl(url);
            if (link != null)
                //throw new ArgumentException($"ОШИБКА: Повторная ссылка - {url}");
                return;


            link = new Link()
            {
                URL = url
            };

            if (fatherLink == null) 
            {
                link.IterationId = 0;
                link.FatherLink = null;
            }
            else
            {
                link.IterationId = fatherLink.IterationId + 1;
                link.FatherLink = fatherLink;
            }

            this.repo.Add(link);
            this.repo.SaveChanges();
        }

        public List<string> CallParsingFathersLink(Link link)
        {
            List<string> callLink = new List<string>();
            while (link.FatherLink.IterationId >= 0)
            {
                callLink.Add($"lvlLink {link.IterationId}. url - {link.URL}");
                link = link.FatherLink;
            }
            callLink.Reverse();
            return callLink;
        }

        public void RunSingle(string url)
        {
            Link fatherLink = this.repo.GetByURl(url);

            if (fatherLink == null) // настройка от базовой ссылки
            {
                startDomen = url.Split(new string[] { "/wiki/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                SaveTagsIntoDb(url);
                fatherLink = this.repo.GetByURl(url);           
            }

            var page = DownloadPage(url);
            var urls = ExtractHtmlTags(page, url);
            foreach (var u in urls)
            {
                SaveTagsIntoDb(u, fatherLink);
            }
            Console.WriteLine($"{url} отработана");
        }     

        public ICollection<Link> GetLinks()
        {
            return this.repo.GetAllLinksWithoutChildren();
        }
    }
}
