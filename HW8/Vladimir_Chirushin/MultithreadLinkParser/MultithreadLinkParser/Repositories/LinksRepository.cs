namespace MultithreadLinkParser.Repositories
{
    using MultithreadLinkParser.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class LinksRepository : ILinksRepository
    {
        public bool Insert(LinkInfo linkInfo)
        {
            using (var db = new LinksParserContext())
            {
                db.Links.Add(linkInfo);
                db.SaveChanges();
                return true;
            }
        }

        public async Task<bool> IsExistAsync(string link)
        {
            using (var db = new LinksParserContext())
            {
                return  await (db.Links.Where(l => l.urlString == link).FirstOrDefaultAsync()) != null;
            }
        }

        public void LinksInsertAsync(List<LinkInfo> linkInfo)
        {
            using (var db = new LinksParserContext())
            {
                db.Links.AddRange(linkInfo);
                db.SaveChanges();
            }
        }
    }
}