namespace MultithreadLinkParser.Core.Services
{
    using MultithreadLinkParser.Core.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITagsDataBaseManager
    {
        //Task RunLinkToDBAdderAsync(List<LinkInfo> linkInfos, CancellationToken cts);

        Task<bool> AddLinksAsync(List<string> linkInfos, int linkLayer, CancellationToken cts);
    }
}