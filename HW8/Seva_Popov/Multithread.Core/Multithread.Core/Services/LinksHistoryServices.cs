using Multithread.Core.Models;
using Multithread.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.Core.Services
{
    public class LinksHistoryServices
    {
        private readonly ILinksHistoryRepositoroes linksHistoryRepositoroes;

        public LinksHistoryServices(ILinksHistoryRepositoroes linksHistoryRepositoroes)
        {
            this.linksHistoryRepositoroes = linksHistoryRepositoroes;
        }

        public void RegisterNewLinks(LinksHistoryEntity linksHistoryEntity)
        {
            if (this.linksHistoryRepositoroes.Contains(linksHistoryEntity))
            {
                throw new ArgumentException("This links has been registered. Can't continue");
            }

            this.linksHistoryRepositoroes.Add(linksHistoryEntity);

            this.linksHistoryRepositoroes.SaveChanges();
        }

        public bool ContainsLinks(LinksHistoryEntity linksHistoryEntity)
        {
           return this.linksHistoryRepositoroes.Contains(linksHistoryEntity);
        }
    }
}
