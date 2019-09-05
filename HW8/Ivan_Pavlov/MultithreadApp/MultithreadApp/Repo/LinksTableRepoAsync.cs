namespace MultithreadApp.Repo
{
 using MultithreadApp.Core.Model;
    using MultithreadApp.Core.Repo;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LinksTableRepoAsync : ILinksTableRepoAsync
    {
        private readonly LinksDbContext db;

        public LinksTableRepoAsync(LinksDbContext db)
        {
            this.db = db;
        }

        public async Task<Link> GetById(int id)
        {
            return await this.db.Links.FindAsync(id);
        }

        public async Task<Link> GetByURl(string URl)
        {
            //return this.db.Links.Where(l => l.URL == URl).FirstOrDefault();
            return await this.db.Links.FindAsync(URl);
        }

        public async void Add(Link entity)
        {
             this.db.Links.Add(entity);
        }

        public int Count()
        {
            return db.Links.Count();
        }

        public async void SaveChanges()
        {
           await this.db.SaveChangesAsync();
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
