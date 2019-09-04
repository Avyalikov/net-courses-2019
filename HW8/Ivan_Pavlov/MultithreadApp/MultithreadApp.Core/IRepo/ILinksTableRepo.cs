namespace MultithreadApp.Core.Repo
{
    using System.Collections.Generic;
    using MultithreadApp.Core.Model;

    public interface ILinksTableRepo
    {
        int Count();
        void Add(Link entity);
        void SaveChanges();
        Link GetByURl(string URL);
        Link GetById(int id);
        ICollection<Link> GetAllLinksWithoutChildren();
    }
}
