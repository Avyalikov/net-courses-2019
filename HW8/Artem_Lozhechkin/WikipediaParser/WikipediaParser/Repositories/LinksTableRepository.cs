using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikipediaParser.Models;

namespace WikipediaParser.Repositories
{
    public class LinksTableRepository
    {
        private readonly WikiParsingDbContext dbContext;
        private readonly object locker = new object();
        public LinksTableRepository(WikiParsingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(LinkEntity linkEntity)
        {
            lock (locker)
            {
                this.dbContext.Links.Add(linkEntity);
                return SaveChanges();
            }
        }
        public LinkEntity GetById(int id)
        {
            lock (locker)
            {
                return this.dbContext.Links.Find(id);
            }
        }
        public bool ContainsByUrl(LinkEntity linkEntity)
        {
            lock(locker)
            {
                return this.dbContext.Links.Any(link => link.Link == linkEntity.Link);
            }
        }
        public int SaveChanges()
        {
            lock (locker)
            {
                return this.dbContext.SaveChanges();
            }
        }
    }
}
