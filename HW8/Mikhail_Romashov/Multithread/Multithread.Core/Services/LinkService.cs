using Multithread.Core.Dto;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.Core.Services
{
    public class LinkService
    {
        private readonly ILinkTableRepository linkTableRepository;

        public LinkService(ILinkTableRepository linkTableRepository)
        {
            this.linkTableRepository = linkTableRepository;
        }

        public void SaveChanges()
        {
            this.linkTableRepository.SaveChanges();
        }
        public int AddNewLink(LinkInfo linkInfo)
        {
            var entityToAdd = new LinkEntity()
            {
                Link = linkInfo.Link,
                Iteration = linkInfo.Iteration
            };

            this.linkTableRepository.Add(entityToAdd);
            this.linkTableRepository.SaveChanges();
            return entityToAdd.Id;
        }
        public bool ContainsByLink(string link)
        {
            return this.linkTableRepository.Contains(link);
        }

        public IEnumerable<LinkEntity> GetListOfLinks()
        {
            return this.linkTableRepository.GetListOfLinks();
        }

        public LinkEntity GetLinkById(int id)
        {
            if (!this.linkTableRepository.ContainsById(id))
                throw new ArgumentException($"Can`t find item by this id = {id}");
            return this.linkTableRepository.GetById(id);
        }

        public IEnumerable<LinkEntity> GetListOfLinksByIteration(int iteration)
        {
            return linkTableRepository.GetListOfLinksByIteration(iteration);
        }
    }
}
