namespace MultithreadLinksApp.Core.Services
{
    using Interfaces;
    using Models;
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;

    public class URLParsingService : IURLParsingService
    {
        public IEnumerable<URL> GetAllURLsFromPage(string page, int iteration)
        {
            List<URL> urlsFromPage = new List<URL>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            IEnumerable<HtmlNode> allUrls = doc.DocumentNode.SelectNodes("//a[@href]");
            var filtredUrls = allUrls.Where(a => a.Attributes["href"].Value.StartsWith("/wiki/")).Where((a) =>
            {
                if (a.ParentNode.Attributes["class"] != null)
                {
                    return a.ParentNode.Attributes["class"].Value.Equals("mw-redirect");
                }

                if (a.ParentNode.Attributes.Count() != 0)
                {
                    return false;
                }

                return true;
            });

            if (filtredUrls.Count() == 0)
            {
                return null;
            }

            foreach (var url in filtredUrls)
            {
                if (urlsFromPage.Where(x => x.Url == url.Attributes["href"].Value).Count() == 0)
                {
                    URL newUrl = new URL() { Url = url.Attributes["href"].Value, IterationID = iteration };
                    urlsFromPage.Add(newUrl);
                }
            }

            if (urlsFromPage.Count() > 0)
            {
                return urlsFromPage;
            }
                
            return null;
        }
    }
}
