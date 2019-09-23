using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Models;
using TradeSimulator.Core.Repositories;
using TradeSimulator.Server.DbInit;

namespace TradeSimulator.Server.Repositories
{
    public class StockPriceTableRepository : IStockPriceTableRepository
    {
        private readonly TradeSimDbContext dbContext;

        public StockPriceTableRepository(TradeSimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(StockPriceEntity entity)
        {
            dbContext.StockPrice.Add(entity);
        }

        public ICollection<StockPriceEntity> GetAllStockPrice()
        {
            return dbContext.StockPrice.ToList();
        }

        public StockPriceEntity GetStockPriceEntityByStockType(string typeOfStock)
        {
            return dbContext.StockPrice.DefaultIfEmpty(null).FirstOrDefault(f => f.TypeOfStock == typeOfStock);
        }

        public void Remove(StockPriceEntity entity)
        {
            dbContext.StockPrice.Remove(entity);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }                
    }
}
