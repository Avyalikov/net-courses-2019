using System.Threading.Tasks;
using WikipediaParser.DTO;

namespace WikipediaParser.Services
{
    public interface IDatasourceManagementService
    {
        Task AddToDb(IUnitOfWork uof, LinkInfo linkInfo);
    }
}