using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;


namespace TradingConsoleApp.Repositories
{
    class BalanceTableRepository : IBalanceTableRepository
    {
        private readonly TradingAppDbContext dbContext;

        public BalanceTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(BalanceEntity balanceEntity)
        {
            this.dbContext.Balances.Add(balanceEntity);
        }

        public void Change(BalanceEntity entity)
        {
            var oldEntity = this.dbContext.Balances.Find(entity.BalanceID);
            this.dbContext.Balances.Remove(oldEntity);
            this.dbContext.Balances.Add(entity);
        }

        public bool Contains(string balanceId)
        {
            var entity = this.dbContext.Balances.Find(balanceId);
            return this.dbContext.Balances.Contains(entity);
        }

        public bool Contains(BalanceEntity balanceEntity)
        {
            return this.dbContext.Balances.Contains(balanceEntity);
        }

        public BalanceEntity Get(string balanceId)
        {
           return this.dbContext.Balances.Find(balanceId);
        }

        public List<BalanceEntity> GetAll(string userId)
        {
            List<BalanceEntity> arr = new List<BalanceEntity>();
            foreach(BalanceEntity entity in this.dbContext.Balances)
            {
                if (entity.UserID == userId) arr.Add(entity);
            }
            return arr;
            
        }

        public decimal GetBalance(string balanceId)
        {
            var entity = this.dbContext.Balances.Find(balanceId);
            return entity.Balance;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
