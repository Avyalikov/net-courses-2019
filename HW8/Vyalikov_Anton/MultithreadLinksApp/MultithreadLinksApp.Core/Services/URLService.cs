namespace MultithreadLinksApp.Core.Services
{
    using Interfaces;
    using Models;
    using Repos;
    using System.Collections.Generic;
    using System.Linq;

    public class URLService : IURLService
    {
        private readonly IURLRepository urlRepos;

        public URLService(IURLRepository urlRepos)
        {
            this.urlRepos = urlRepos;
        }

        public void AddURL(URL url)
        {
            urlRepos.AddURL(url);
            urlRepos.SaveChanges();
        }

        public void AddURLFromDict(Dictionary<string, int> urls)
        {
            var urlsFromDB = urlRepos.GetAllUrls();
            foreach (var url in urls)
            {
                if (!urlsFromDB.Any(x => x.Url == url.Key))
                {
                    var urlEntity = new URL() { Url = url.Key, IterationID = url.Value };
                    AddURL(urlEntity);
                    urlRepos.SaveChanges();
                }
            }
        }

        public void RemoveURL(string url)
        {
            urlRepos.RemoveURL(url);
            urlRepos.SaveChanges();
        }

        public URL GetURL(string url)
        {
            return urlRepos.GetURL(url);
        }
    }
}
