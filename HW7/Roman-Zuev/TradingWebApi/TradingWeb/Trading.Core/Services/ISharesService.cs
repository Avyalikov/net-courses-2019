using Trading.Core.Models;

namespace Trading.Core.Services
{
    public interface ISharesService
    {
        void AddNewShares(SharesEntity sharesToAdd);
        void UpdateShares(SharesEntity sharesToAdd);
        void RemoveShares(SharesEntity sharesToRemove);
    }
}