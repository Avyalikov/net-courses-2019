using MultithreadApp.Console.DbInit;
using MultithreadApp.Core.Model;
using MultithreadApp.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Console.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly MultiAppDbContext dbcontext = new MultiAppDbContext();

        public UrlRepository(MultiAppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public void Add(Url url)
        {
            this.dbcontext.Urls.Add(url);
        }


        public IEnumerable<Url> GetAll()
        {
            return this.dbcontext.Urls.ToList();
        }


        public IEnumerable<Url> GetByCondition(Func<Url, bool> predicate)
        {
            return this.dbcontext.Urls.AsNoTracking().Where(predicate).ToList();
        }
    }
}
