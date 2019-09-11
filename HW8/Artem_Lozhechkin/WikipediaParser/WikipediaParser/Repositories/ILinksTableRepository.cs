using System.Threading.Tasks;
using WikipediaParser.Models;

namespace WikipediaParser.Repositories
{
    public interface ILinksTableRepository
    {
        Task<int> AddAsync(LinkEntity linkEntity);
        Task<bool> ContainsByUrl(LinkEntity linkEntity);
        LinkEntity GetById(int id);
        Task<int> SaveChangesAsync();
    }
}