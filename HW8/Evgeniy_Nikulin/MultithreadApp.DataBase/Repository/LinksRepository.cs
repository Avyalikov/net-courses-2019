namespace MultithreadApp.DataBase.Repository
{
    using System.Linq;
    using Repository.Core;
    using Repository.Interface;
    using Model;
    
    internal class LinksRepository : Repository<Links>, ILinksRepository
    {
        public LinksRepository(SiteDbContext db) : base(db)
        {

        }

        public bool IsExist(string link)
        {
            var str = this.db.Links
                .Where(l => l.link == link)
                .SingleOrDefault();
            
            return str != null;
        }
    }
}