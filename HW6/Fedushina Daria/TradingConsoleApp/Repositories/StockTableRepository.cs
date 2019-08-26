using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingConsoleApp.Repositories
{
    class StockTableRepository : IStockTableRepository
    {
        private readonly TradingAppDbContext dbContext;
        public StockTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        

        public void Add(StockEntity entity)
        {
            this.dbContext.Stocks.Add(entity);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public bool Contains(StockEntity entity)
        {
            return this.dbContext.Stocks.Any(f =>
            f.Type == entity.Type &&
            f.Price == entity.Price);
        }

        public bool Contains(int entityId)
        {
            var entity = this.dbContext.Stocks.Find(entityId);
            return this.dbContext.Stocks.Any(f =>
            f.Type == entity.Type &&
            f.Price == entity.Price);
        }

        public StockEntity Get(int stockId)
        {
            return this.dbContext.Stocks.Find(stockId);
        }
    }
}
