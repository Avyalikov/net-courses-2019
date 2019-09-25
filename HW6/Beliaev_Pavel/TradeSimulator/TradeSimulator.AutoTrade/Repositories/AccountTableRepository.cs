using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.AutoTrade.DbInit;
using TradeSimulator.Core.Models;
using TradeSimulator.Core.Repositories;

namespace TradeSimulator.AutoTrade.Repositories
{
     public class AccountTableRepository : IAccountTableRepository
    {
        private readonly TradeSimDbContext dbContext;

        public AccountTableRepository(TradeSimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(AccountEntity entity)
        {
            dbContext.Account.Add(entity);
        }

        public void Change(AccountEntity entity)
        {
            var accountToChange = dbContext.Account.First(f => f.ClientId == entity.ClientId);
            accountToChange.Balance = entity.Balance;
            accountToChange.Stocks = entity.Stocks;
            accountToChange.Zone = entity.Zone;
        }
        
        public AccountEntity GetAccountByClientId(int clientId)
        {
            return this.dbContext.Account.DefaultIfEmpty(null).FirstOrDefault(f=>f.ClientId == clientId);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
