namespace MultithreadLinkParser.Services
{
    using MultithreadLinkParser.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ILinkToDBManager
    {
        Task<bool> AddLinksAsync(List<LinkInfo> linkInfos, CancellationToken cts);

        Task RunLinkToDBAdderAsync(List<LinkInfo> linkInfos, CancellationToken cts);
    }
}