namespace MultithreadLinkParser.Core.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITagsDataBaseManager
    {
        Task<bool> AddLinksAsync(List<string> linkInfos, int linkLayer, CancellationToken cts);
    }
}