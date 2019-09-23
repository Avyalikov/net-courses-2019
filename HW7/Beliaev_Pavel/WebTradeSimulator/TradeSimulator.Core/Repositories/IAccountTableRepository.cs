namespace TradeSimulator.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public interface IAccountTableRepository
    {
        void Add(AccountEntity entity);
        void Remove(AccountEntity entity);
        void SaveChanges();

        AccountEntity GetAccountByClientId(int clientId);
        int GetClientIdByAccountId(int accountId);
    }
}
