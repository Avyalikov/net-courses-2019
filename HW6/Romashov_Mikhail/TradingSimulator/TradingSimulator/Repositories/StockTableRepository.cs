﻿using System.Linq;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
namespace TradingSimulator.Repositories
{
    class StockTableRepository : IStockTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public StockTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool Contains(StockEntity entity)
        {
            return this.dbContext.Stocks.Any(s => s.Name == entity.Name);
        }

        public bool ContainsById(int entityId)
        {
            return this.dbContext.Stocks.Any(s => s.Id == entityId);
        }

        public StockEntity Get(int stockID)
        {
            var Item = this.dbContext.Stocks.First(t => t.Id == stockID);
            return Item;
        }
    }
}
