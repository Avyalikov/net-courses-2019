namespace MultithreadLinkParser.Console.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using MultithreadLinkParser.Core.Models;
    using MultithreadLinkParser.Core.Repositories;
    
    public class TagsRepository : ITagsRepository
    {
        public void Insert(LinkInfo linkInfo)
        {
            using (var db = new LinksParserContext())
            {
                db.Links.Add(linkInfo);
                db.SaveChanges();
            }
        }

        public async Task<bool> IsExistAsync(string link)
        {
            using (var db = new LinksParserContext())
            {
                return await db.Links.Where(l => l.UrlString == link).FirstOrDefaultAsync() != null;
            }
        }

        public async void LinksInsertAsync(List<LinkInfo> linkInfo)
        {
            using (var db = new LinksParserContext())
            {
                db.Links.AddRange(linkInfo);
                db.SaveChangesAsync();
            }
        }
    }
}