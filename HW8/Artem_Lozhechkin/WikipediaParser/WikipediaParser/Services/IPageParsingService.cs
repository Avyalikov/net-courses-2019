using System.Collections.Generic;
using System.Threading.Tasks;
using WikipediaParser.DTO;

namespace WikipediaParser.Services
{
    public interface IPageParsingService
    {
        Task<List<LinkInfo>> ExtractTagsFromFile(IUnitOfWork uof, LinkInfo linkInfo);
        Task<List<LinkInfo>> ExtractTagsFromSource(IUnitOfWork uof, LinkInfo linkInfo);
    }
}