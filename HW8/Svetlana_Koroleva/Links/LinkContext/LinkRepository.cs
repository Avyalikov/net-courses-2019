using LinkDBContext.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlLinksCore.Model;
using UrlLinksCore.Repository;

namespace LinkDBContext
{
   public class LinkRepository : ILinkRepository
    {
        private readonly LinksContext dbcontext = new LinksContext();

        public LinkRepository(LinksContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public void Add(Link link)
        {
            this.dbcontext.Links.Add(link);
        }


        public IEnumerable<Link> GetAll()
        {
            return this.dbcontext.Links.ToList();
        }


        public IEnumerable<Link> GetByCondition(Func<Link, bool> predicate)
        {
            return this.dbcontext.Links.AsNoTracking().Where(predicate).ToList();
        }
    }
}
