namespace MultithreadLinkParser.Services
{
    using MultithreadLinkParser.Models;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ILinkParserService
    {
        Task AccessTheWebAsync(List<LinkInfo> linkInfo, List<string> urlList, CancellationToken ct, int currentLayer);

        Task<List<string>> ProcessURL(string urlToParse, HttpClient client, CancellationToken ct);
    }
}