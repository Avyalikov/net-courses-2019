namespace MultithreadLinkParser.Core.Repositories
{
    using MultithreadLinkParser.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagsRepository
    {
        bool Insert(LinkInfo linkInfo);

        Task<bool> IsExistAsync(string link);

        void LinksInsertAsync(List<LinkInfo> linkInfo);
    }
}