using System.Linq;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Repositories
{
    class TraderStockTableRepository : ITraderStockTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public TraderStockTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(StockToTraderEntity entityToAdd)
        {
            this.dbContext.TraderStocks.Add(entityToAdd);
        }

        public bool Contains(StockToTraderEntity stockToTraderEntity)
        {
            return this.dbContext.TraderStocks.Any(t =>
             t.TraderId == stockToTraderEntity.TraderId
           && t.StockId == stockToTraderEntity.StockId);
        }

        public bool Contains(BuyArguments args)
        {
            return this.dbContext.TraderStocks.Any(t =>
                t.TraderId == args.CustomerID
                && t.StockId == args.StockID);
        }

        public StockToTraderEntity FindStocksFromSeller(BuyArguments buyArguments)
        {
            var item = this.dbContext.TraderStocks.First(t => t.TraderId == buyArguments.SellerID
                    && t.StockId == buyArguments.StockID);
            return item;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void SubtractStock(BuyArguments args)
        {
            var ItemToUpdate = this.dbContext.TraderStocks.First(t =>
                t.TraderId == args.SellerID
                && t.StockId == args.StockID);
            ItemToUpdate.StockCount -= args.StockCount;
        }
    }
}
