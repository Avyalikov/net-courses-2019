namespace MultithreadApp.Repo
{
    using MultithreadApp.Core.Model;
    using MultithreadApp.Core.Repo;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LinksTableRepo : ILinksTableRepo
    {
        private readonly LinksDbContext db;

        public LinksTableRepo(LinksDbContext db)
        {
            this.db = db;
        }

        public Link GetById(int id)
        {
            return this.db.Links.Find(id);
        }

        public Link GetByURl(string URl)
        {
            return this.db.Links.Where(l => l.URL == URl).FirstOrDefault();
        }

        public void Add(Link entity)
        {
            this.db.Links.Add(entity);
        }

        public int Count()
        {
            return db.Links.Count();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public ICollection<Link> GetAllLinksWithoutChildren()
        {
            if (this.db.Links.Count() == 0)
                return null;
            else
            {
                int maxIteration = db.Links.Max(l => l.IterationId);
                return db.Links.Where(l => l.IterationId == maxIteration).ToList();
            }
        }
    }
}
