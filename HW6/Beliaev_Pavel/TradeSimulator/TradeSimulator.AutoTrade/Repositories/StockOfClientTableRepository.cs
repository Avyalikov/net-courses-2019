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
    public class StockOfClientTableRepository : IStockOfClientTableRepository
    {
        private readonly TradeSimDbContext dbContext;

        public StockOfClientTableRepository(TradeSimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(StockOfClientEntity entity)
        {
            dbContext.StockOfClient.Add(entity);
        }

        public void Delete(StockOfClientEntity entity)
        {
            dbContext.StockOfClient.Remove(entity);
        }

        public ICollection<StockOfClientEntity> GetAllStockOfClient()
        {
            return dbContext.StockOfClient.OrderBy(o=>o.AccountForStock.ClientId).ToArray();
        }

        public ICollection<StockOfClientEntity> GetFullStockOfClientByAccountId(int accountId)
        {
            return dbContext.StockOfClient.Where(w => w.AccountId == accountId).ToArray();
        }

        public ICollection<StockOfClientEntity> GetFullStockOfClientByClientId(int clientId)
        {
            return dbContext.StockOfClient.Where(w=>w.AccountForStock.ClientId == clientId).ToArray();
        }

        public StockOfClientEntity GetStockOfClientEntityByAccountIdAndType(int accountId, string typeOfStock)
        {
            return dbContext.StockOfClient.DefaultIfEmpty(null).First(w => (w.AccountId == accountId) && (w.TypeOfStocks == typeOfStock));
        }

        public StockOfClientEntity GetStockOfClientEntityByClientIdAndType(int clientId, string typeOfStock)
        {
            return dbContext.StockOfClient.DefaultIfEmpty(null).First(w => (w.AccountForStock.ClientId == clientId) && (w.TypeOfStocks == typeOfStock));
        }
        
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
