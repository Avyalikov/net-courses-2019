// <copyright file="LinkService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace LinkContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.DTO;
    using UrlLinksCore.Model;
    using UrlLinksCore.Repository;
    using UrlLinksCore.Service;

    /// <summary>
    /// LinkService description
    /// </summary>
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository linkRepository;

        public LinkService(ILinkRepository linkRepository)
        {
            this.linkRepository = linkRepository;
        }

        public void AddLinkToDB(LinkDTO linkDTO)
        {
            if (this.ContainsLink(linkDTO.Link))
            {
               return;
            }

            Link linkToAdd = new Link()
            {
                Url = linkDTO.Link,
                IterationId = linkDTO.IterationId

            };
            this.linkRepository.Add(linkToAdd);
            this.linkRepository.Save();
        }

        public bool ContainsLink(string link)
        {
            return this.linkRepository.GetByCondition(l => l.Url == link).Count()!=0;
        }

        public IEnumerable<Link> GetAllLinks()
        {
          return  this.linkRepository.GetAll().ToList();
        }

        public  IEnumerable<Link> GetAllLinksByIteration(int iterationId)
        {
            return this.linkRepository.GetByCondition(l => l.IterationId == iterationId).ToList();
        }

        public IEnumerable<int> GetIterations()
        {
            var links = this.GetAllLinks().ToList();
            IEnumerable<int> iterations = links.Select(i=>i.IterationId).Distinct().ToList();
            return iterations;
        }
    }
}
