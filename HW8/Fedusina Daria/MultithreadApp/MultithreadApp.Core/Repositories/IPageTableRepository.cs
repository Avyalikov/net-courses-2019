using MultithreadApp.Core.Models;

namespace MultithreadApp.Core.Repositories
{
    public interface IPageTableRepository
    {
       // string DownLoadPage(string url);
        bool Contains(PageEntity entityToAdd);
        void Add(PageEntity entityToAdd);
        void SaveChanges();
    }
}