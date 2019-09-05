﻿using Multithread.Core.Models;
using Multithread.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MultithreadConsoleApp.Repositories
{
    class LinkRepository : ILinkTableRepository
    {
        private readonly LinksDBContext dbContext;

        public LinkRepository(LinksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(LinkEntity entity)
        {
            this.dbContext.Links.Add(entity);
        }

        public bool Contains(string link)
        {
            return this.dbContext.Links.Any(l => l.Link == link);
        }

        public bool ContainsById(int linkId)
        {
            return this.dbContext.Links.Any(l => l.Id == linkId);
        }

        public LinkEntity GetById(int linkId)
        {
            return this.dbContext.Links.First(l => l.Id == linkId);
        }

        public List<LinkEntity> GetListOfLinks()
        {
            List<LinkEntity> listItems = new List<LinkEntity>();
            foreach (var item in this.dbContext.Links)
            {
                listItems.Add(item);
            }

            return listItems;
        }

        public List<LinkEntity> GetListOfLinksByIteration(int iteration)
        {
            List<LinkEntity> listItems = new List<LinkEntity>();
            foreach (var item in this.dbContext.Links.Where(l => l.Iteration == iteration))
            {
                listItems.Add(item);
            }

            return listItems;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
