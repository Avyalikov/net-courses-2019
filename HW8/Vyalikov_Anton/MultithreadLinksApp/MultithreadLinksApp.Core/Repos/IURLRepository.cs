namespace MultithreadLinksApp.Core.Repos
{
    using Models;
    using System.Collections.Generic;

    public interface IURLRepository : IRepository
    {
        IEnumerable<URL> GetAllUrls();
        URL GetURL(string url);
        void AddURL(URL url);
        void UpdateURL(URL url);
        void RemoveURL(string url);
    }
}
