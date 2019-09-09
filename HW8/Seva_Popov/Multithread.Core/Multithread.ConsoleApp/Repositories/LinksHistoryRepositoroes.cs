using Multithread.ConsoleApp.Data;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Multithread.ConsoleApp.Repositories
{
    public class LinksHistoryRepositoroes : ILinksHistoryRepositoroes
    {
        //private readonly MultithreadDbContext dbContext;

        //public LinksHistoryRepositoroes(MultithreadDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}

        public void Add(LinksHistoryEntity linksHistoryEntity)
        {
            using (MultithreadDbContext dbContext = new MultithreadDbContext())
            {
                dbContext.Links.Add(linksHistoryEntity);
                dbContext.SaveChanges();
            }               
        }

        public bool Contains(LinksHistoryEntity linksHistoryEntity)
        {
            using (MultithreadDbContext dbContext = new MultithreadDbContext())
            {
                return dbContext.Links.Any(f =>
                f.Links == linksHistoryEntity.Links);
            }    
        }

        public void SaveChanges()
        {
            using (MultithreadDbContext dbContext = new MultithreadDbContext())
            {
                dbContext.SaveChanges();
            }
            
        }
    }
}
