namespace MultithreadLinkParser.Repositories
{
    using MultithreadLinkParser.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILinksRepository
    {
        bool Insert(LinkInfo linkInfo);
        Task<bool> IsExistAsync(string link);
        void LinksInsertAsync(List<LinkInfo> linkInfo);
    }
}