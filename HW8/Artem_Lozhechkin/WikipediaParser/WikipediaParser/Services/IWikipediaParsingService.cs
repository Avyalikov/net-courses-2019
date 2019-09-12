using System.Threading.Tasks;
using WikipediaParser.DTO;

namespace WikipediaParser.Services
{
    public interface IWikipediaParsingService
    {
        void Start(string baseUrl);
        Task ProcessUrlRecursive(LinkInfo link);
    }
}