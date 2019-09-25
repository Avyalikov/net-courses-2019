using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Models;
using WikiURLCollector.Core.Repositories;

namespace WikiURLCollector.ConsoleApp.Repositories
{
    public class UrlRepository: BaseRepository, IUrlRepository
    {
        private readonly WikiUrlDbContext dbContext;
        public UrlRepository(WikiUrlDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddUrlEntity(UrlEntity urlEntity)
        {
            dbContext.Urls.Add(urlEntity);
        }

        public IEnumerable<UrlEntity> GetAllUrls()
        {
            return dbContext.Urls;
        }

        public UrlEntity GetUrlEntity(string Url)
        {
            return dbContext.Urls.Where(u => u.URL == Url).FirstOrDefault();
        }

        public void RemoveUrlEntity(string Url)
        {
            dbContext.Urls.Remove(GetUrlEntity(Url));
        }

        public void UpdateUrlEntity(UrlEntity urlEntity)
        {
            var clientOld = GetUrlEntity(urlEntity.URL);
            dbContext.Entry(clientOld).CurrentValues.SetValues(urlEntity);
        }
    }
}
