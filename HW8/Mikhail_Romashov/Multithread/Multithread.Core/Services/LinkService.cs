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

            if (this.linkTableRepository.Contains(linkInfo.Link))
            {
                throw new ArgumentException($"This link {entityToAdd.Link} has been added");
            }

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
      
        public LinkEntity GetTraderById(int linkId)
        {
            if (!linkTableRepository.ContainsById(linkId))
            {
                throw new ArgumentException($"Can`t get link by this Id = {linkId}.");
            }
            return linkTableRepository.GetById(linkId);
        }
        public IEnumerable<LinkEntity> GetListOfLinksByIteration(int iteration)
        {
            return linkTableRepository.GetListOfLinksByIteration(iteration);
        }
    }
}
