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
                var newList = list.ToList();
                return newList;
            }

            return emptyList;
        }

        public Dictionary<string, int> LookingForDuplicateLinkStrings()
        {
            Dictionary<string, int> resultList = new Dictionary<string, int>();

            var linkGroups = this.dBContext.Links.GroupBy(lnk => lnk.Link)
                        .Select(g => new { LinkName = g.Key, Count = g.Count() }).Where(s => s.Count > 1);

            if (linkGroups != null)
            {
                resultList = linkGroups.ToDictionary(k => k.LinkName, v => v.Count);
            }

            return resultList;
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
