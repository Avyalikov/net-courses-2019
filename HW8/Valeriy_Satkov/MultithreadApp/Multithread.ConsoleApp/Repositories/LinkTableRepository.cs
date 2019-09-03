namespace Multithread.ConsoleApp.Repositories
{
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LinkTableRepository : ILinkTableRepository
    {
        private readonly ConnectedLinksDBContext dBContext;

        public LinkTableRepository(ConnectedLinksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(LinkEntity linkEntity)
        {
            throw new NotImplementedException();
        }

        public bool ContainsByLink(string link)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
