namespace WikipediaParser.Services
{
    using System.Threading.Tasks;
    using WikipediaParser.DTO;

    public interface IDownloadingService
    {
        Task<LinkInfo> DownloadSourceAsString(LinkInfo link);
        Task<string> DownloadSourceToFile(LinkInfo link);
    }
}