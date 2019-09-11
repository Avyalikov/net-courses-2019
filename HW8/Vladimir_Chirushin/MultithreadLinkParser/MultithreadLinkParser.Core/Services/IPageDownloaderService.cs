namespace MultithreadLinkParser.Core.Services
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPageDownloaderService
    {
        Task<string> DownloadPage(string urlToParse, HttpClient client, CancellationToken cts);
    }
}