using System.Threading.Tasks;
using WikipediaParser.DTO;

namespace WikipediaParser.Services
{
    public interface IDownloadingService
    {
        Task<LinkInfo> DownloadSourceAsString(LinkInfo link);
        Task<string> DownloadSourceToFile(LinkInfo link);
    }
}