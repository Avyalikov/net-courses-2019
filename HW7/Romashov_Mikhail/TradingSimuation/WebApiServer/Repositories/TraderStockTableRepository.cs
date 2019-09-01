using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace WebApiServer.Repositories
{
    class TraderStockTableRepository : ITraderStockTableRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public TraderStockTableRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(StockToTraderEntityDB entityToAdd)
        {
            this.dbContext.TraderStocks.Add(entityToAdd);
        }

        public bool Contains(StockToTraderEntityDB stockToTraderEntity)
        {
            return this.dbContext.TraderStocks.Any(t =>
             t.TraderId == stockToTraderEntity.TraderId
           && t.StockId == stockToTraderEntity.StockId);
        }

        public bool ContainsById(int id)
        {
            return this.dbContext.TraderStocks.Any(t =>
             t.Id == id);
        }

        public bool ContainsSeller(BuyArguments args)
        {
            return this.dbContext.TraderStocks.Any(t =>
                t.TraderId == args.SellerID
                && t.StockId == args.StockID);
        }

        public bool ContainsCustomer(BuyArguments args)
        {
            return this.dbContext.TraderStocks.Any(t =>
                t.TraderId == args.CustomerID
                && t.StockId == args.StockID);
        }

        public StockToTraderEntityDB GetStocksFromSeller(BuyArguments buyArguments)
        {
            var item = this.dbContext.TraderStocks.First(t => t.TraderId == buyArguments.SellerID
                    && t.StockId == buyArguments.StockID);
            return item;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void SubtractStockFromSeller(BuyArguments args)
        {
            var ItemToUpdate = this.dbContext.TraderStocks.First(t =>
                t.TraderId == args.SellerID
                && t.StockId == args.StockID);
            ItemToUpdate.StockCount -= args.StockCount;
        }

        public void AdditionalStockToCustomer(BuyArguments args)
        {
            var ItemToUpdate = this.dbContext.TraderStocks.First(t =>
                t.TraderId == args.CustomerID
                && t.StockId == args.StockID);
            ItemToUpdate.StockCount += args.StockCount;
        }

        public List<int> GetListOfTraderStocksIds()
        {
            List<int> listItems = new List<int>();
            foreach (var item in this.dbContext.TraderStocks)
            {
                listItems.Add(item.Id);
            }

            return listItems;
        }

        public int GetCountOfListOfTraderStocksIds()
        {
            var count = this.dbContext.TraderStocks.Count();
            return count;
        }

        public IEnumerable<StockToTraderEntityDB> GetTradersStockById(int traderId)
        {
            return this.dbContext.TraderStocks.Where(t => t.TraderId == traderId);
        }

        public StockToTraderEntityDB GetTraderStockById(int id)
        {
            return this.dbContext.TraderStocks.First(t => t.Id == id);
        }
    }
}
