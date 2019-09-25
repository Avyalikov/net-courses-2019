using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.ConsoleApp.DbInit;
using TradeSimulator.Core.Models;
using TradeSimulator.Core.Repositories;

namespace TradeSimulator.ConsoleApp.Repositories
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

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
