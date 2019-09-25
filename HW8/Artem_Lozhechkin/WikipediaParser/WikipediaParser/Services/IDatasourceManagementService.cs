namespace WikipediaParser.Services
{
    using System.Threading.Tasks;
    using WikipediaParser.DTO;

    public interface IDatasourceManagementService
    {
        Task AddToDb(IUnitOfWork uof, LinkInfo linkInfo);
    }
}