using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IBalanceTableRepository
    {
        bool Contains(BalanceEntity entity); // compare by client ID. Only one balance for each client
        void Add(BalanceEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        BalanceEntity Get(int entityId);
        IEnumerable<BalanceEntity> GetZeroBalances();
        IEnumerable<BalanceEntity> GetNegativeBalances();
        // BalanceEntity GetByClient(int clientEntityId); // not implemented
        void ChangeAmount(int entityId, decimal newAmount);
    }
}
