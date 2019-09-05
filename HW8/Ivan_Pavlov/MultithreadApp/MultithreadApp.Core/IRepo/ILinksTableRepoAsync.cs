using System.Collections.Generic;
using System.Threading.Tasks;
using MultithreadApp.Core.Model;

namespace MultithreadApp.Repo
{
    public interface ILinksTableRepoAsync
    {
        void Add(Link entity);
        int Count();
        ICollection<Link> GetAllLinksWithoutChildren();
        Task<Link> GetById(int id);
        Task<Link> GetByURl(string URl);
        void SaveChanges();
    }
}