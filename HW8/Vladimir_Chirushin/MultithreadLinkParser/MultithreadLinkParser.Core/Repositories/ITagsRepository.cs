namespace MultithreadLinkParser.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MultithreadLinkParser.Core.Models;

    public interface ITagsRepository
    {
        void Insert(LinkInfo linkInfo);

        Task<bool> IsExistAsync(string link);

        void LinksInsertAsync(List<LinkInfo> linkInfo);
    }
}