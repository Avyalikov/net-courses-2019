namespace Traiding.Core.Repositories
{
    using System.Collections.Generic;
    using Traiding.Core.Models;

    public interface ISharesNumberTableRepository
    {
        // bool Contains(SharesNumberEntity entity); // Compare ClientId && ShareId with entity props
        void Add(SharesNumberEntity entity);
        void SaveChanges();
        // bool ContainsById(int entityId);
        // SharesNumberEntity Get(int entityId);
        SharesNumberEntity SearchSharesNumberForBuy(int shareId, int requiredSharesNumber);
        SharesNumberEntity SearchSharesNumberForAddition(int clientId, int shareId);
        IEnumerable<SharesNumberEntity> GetByClient(int clientId);
        IEnumerable<SharesNumberEntity> GetByShare(int shareId);
        void ChangeNumber(int entityId, int newNumber);
        void Remove(int entityId);
    }
}
