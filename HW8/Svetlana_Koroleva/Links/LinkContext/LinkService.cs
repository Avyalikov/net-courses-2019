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
    using LinkContext.DAL;
    using UrlLinksCore;

    /// <summary>
    /// LinkService description
    /// </summary>
    public class LinkService : ILinkService
    {
        private readonly IUnitOfWork unitOfWork;
        public LinkService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            this.unitOfWork.Links.Add(linkToAdd);
            this.unitOfWork.Save();
        }

        public bool ContainsLink(string link)
        {
            return this.unitOfWork.Links.GetByCondition(l => l.Url == link).Count()!=0;
        }

        public IEnumerable<Link> GetAllLinks()
        {
          return  this.unitOfWork.Links.GetAll().ToList();
        }

       
        public IEnumerable<String> GetAllLinksByIteration(int iterationId)
        {
            return this.unitOfWork.Links.GetByCondition(l => l.IterationId == iterationId).Select(l=>l.Url).ToList();
        }

        public IEnumerable<int> GetIterations()
        {
            var links = this.GetAllLinks().ToList();
            IEnumerable<int> iterations = links.Select(i=>i.IterationId).Distinct().ToList();
            return iterations;
        }
    }
}
