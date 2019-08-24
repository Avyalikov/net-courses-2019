namespace Traiding.Core.Repositories
{
    using System.Collections.Generic;
    using Traiding.Core.Models;

    public interface IBalanceTableRepository
    {
        bool Contains(BalanceEntity entity); // compare by client ID. Only one balance for each client
        void Add(BalanceEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        BalanceEntity Get(int entityId);
        BalanceEntity SearchBalanceByClientId(int clientId);
        IEnumerable<BalanceEntity> GetZeroBalances();
        IEnumerable<BalanceEntity> GetNegativeBalances();
        // BalanceEntity GetByClient(int clientEntityId); // not implemented
        void ChangeAmount(int entityId, decimal newAmount);
    }
}
