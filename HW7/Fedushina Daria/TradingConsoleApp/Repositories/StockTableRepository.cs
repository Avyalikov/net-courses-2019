using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Dto;
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

        public List<StockEntity> GetAll()
        {
            List<StockEntity> arr = new List<StockEntity>();
            for (int i = 1; i < this.dbContext.Stocks.Count() + 1; i++)
            {
                arr.Add(this.dbContext.Stocks.Find(i));
            }
            return arr;
        }

        public int GetId(StockRegistrationInfo stockInfo)
        {
            var entity = this.dbContext.Stocks.First(f =>
            f.Type == stockInfo.Type &&
            f.Price == stockInfo.Price);
            return entity.ID;
        }
        public bool Update(StockEntity stock)
        {
            var result = this.dbContext.Stocks.SingleOrDefault(f => f.ID == stock.ID);
            if (result != null)
            {
                try
                {
                    result.Type = stock.Type;
                    result.Price = stock.Price;
                    this.dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    throw new ArgumentException("Can't add new stock to the base");
                }
            }
            else
            {
                throw new ArgumentException($"Can't find stock with ID {stock.ID}");
            }


        }

        public void Delete(int id)
        {
            var entity = this.dbContext.Stocks.Find(id);
            this.dbContext.Stocks.Remove(entity);
            this.dbContext.SaveChanges();

        }
    }
}
