namespace WikipediaParser.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using WikipediaParser.Models;

    public class LinksTableRepository : ILinksTableRepository
    {
        private readonly WikiParsingDbContext dbContext;
        public LinksTableRepository(WikiParsingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> AddAsync(LinkEntity linkEntity)
        {
            await this.dbContext.Links.AddAsync(linkEntity);
            return await SaveChangesAsync();
        }
        public LinkEntity GetById(int id)
        {
            return this.dbContext.Links.Find(id);
        }
        public bool ContainsByUrl(LinkEntity linkEntity)
        {
            return this.dbContext.Links.Any(link => link.Link == linkEntity.Link);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }
    }
}
