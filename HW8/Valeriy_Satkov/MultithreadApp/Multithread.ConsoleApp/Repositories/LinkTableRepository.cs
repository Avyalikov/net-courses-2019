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
            this.dBContext.Links.Add(linkEntity);
        }

        public bool ContainsByLink(string link)
        {
            return this.dBContext.Links.Any(lnk => lnk.Link == link);
        }

        public List<LinkEntity> EntityListByIterationId(int iterationId)
        {
            List<LinkEntity> emptyList = new List<LinkEntity>();
            var list = this.dBContext.Links.Where(lnk => lnk.IterationId == iterationId);
            if (list != null)
            {
                list.ToList();
            }

            return emptyList;
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
