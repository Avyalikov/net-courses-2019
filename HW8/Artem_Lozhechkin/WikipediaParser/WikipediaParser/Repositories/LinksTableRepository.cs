using System;
using System.Collections.Generic;
using System.Linq;
using WikipediaParser.Models;

namespace WikipediaParser.Repositories
{
    public class LinksTableRepository
    {
        private readonly WikiParsingDbContext dbContext;
        public LinksTableRepository(WikiParsingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(LinkEntity linkEntity)
        {
            this.dbContext.Links.Add(linkEntity);
            return SaveChanges();
        }
        public LinkEntity GetById(int id)
        {
            return this.dbContext.Links.Find(id);
        }
        public bool ContainsByUrl(LinkEntity linkEntity)
        {
            return this.dbContext.Links.Any(link => link.Link == linkEntity.Link);
        }
        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
    }
}
