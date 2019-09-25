namespace WikipediaParser.Repositories
{
    using System.Threading.Tasks;
    using WikipediaParser.Models;

    public interface ILinksTableRepository
    {
        Task<int> AddAsync(LinkEntity linkEntity);
        bool ContainsByUrl(LinkEntity linkEntity);
        LinkEntity GetById(int id);
        Task<int> SaveChangesAsync();
    }
}