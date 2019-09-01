using Trading.Core.Models;

namespace Trading.Core.Services
{
    public interface ISharesService
    {
        void Add(SharesEntity sharesToAdd);
        void Update(SharesEntity sharesToAdd);
        void Remove(SharesEntity sharesToRemove);
    }
}