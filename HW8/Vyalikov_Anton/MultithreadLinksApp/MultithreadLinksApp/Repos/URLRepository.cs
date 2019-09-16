namespace MultithreadLinksApp.Repos
{
    using MultithreadLinksApp.Core.Models;
    using MultithreadLinksApp.Core.Repos;
    using System.Collections.Generic;
    using System.Linq;

    class URLRepository : Repository, IURLRepository
    {
        private readonly AppDBContext appDBContext;

        public URLRepository(AppDBContext appDBContext) : base(appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public IEnumerable<URL> GetAllUrls()
        {
            return appDBContext.URLs;
        }

        public URL GetURL(string url)
        {
            return appDBContext.URLs.Where(x => x.Url == url).FirstOrDefault();
        }

        public void AddURL(URL url)
        {
            appDBContext.URLs.Add(url);
            appDBContext.SaveChanges();
        }

        public void UpdateURL(URL url)
        {
            appDBContext.Entry(GetURL(url.Url)).CurrentValues.SetValues(url);
            appDBContext.SaveChanges();
        }

        public void RemoveURL(string url)
        {
            appDBContext.URLs.Remove(GetURL(url));
            appDBContext.SaveChanges();
        }
    }
}
