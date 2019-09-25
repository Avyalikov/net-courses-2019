using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.WebApp.Repositories
{
    class BalanceRepository : DBTable, IBalanceRepository
    {
        private readonly TradingDBContext dbContext;
        public BalanceRepository(TradingDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(BalanceEntity balance)
        {
            dbContext.Balances.Add(balance);
        }

        public IQueryable<BalanceEntity> LoadAllBalances()
        {
            return dbContext.Balances;
        }

        public BalanceEntity LoadBalanceByID(int ID)
        {
            return dbContext.Balances.Where(x => x.ClientID == ID).FirstOrDefault();
        }

        public void Update(BalanceEntity balance)
        {
            var balanceOld = LoadBalanceByID(balance.ClientID);
            dbContext.Entry(balanceOld).CurrentValues.SetValues(balance);
        }

    }
}
