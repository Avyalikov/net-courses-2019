using MultithreadApp.Core.Models;
using MultithreadApp.Core.Repositories;
using MultithreadApp.Dependencies;
using System.Collections.Generic;
using System.Linq;

namespace MultithreadApp.Repositories
{
    public class PageTableRepository : IPageTableRepository
    {
        private readonly MultithreadAppDbContext dbContext;
        public PageTableRepository(MultithreadAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(PageEntity entityToAdd)
        {
            this.dbContext.Links.Add(entityToAdd);
        }

        public bool Contains(PageEntity entityToAdd)
        {
            return this.dbContext.Links.Any(f =>
            f.Link == entityToAdd.Link &&
            f.IterationId == entityToAdd.IterationId);
        }

        public IEnumerable<PageEntity> GetPagesFromPreviousIteration(int IterationNumber)
        {
            List<PageEntity> listToReturn = new List<PageEntity>();
            foreach (PageEntity entity in this.dbContext.Links)
            {
                if(entity.IterationId == IterationNumber)
                {
                    listToReturn.Add(entity);
                }
            }

            return listToReturn;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}