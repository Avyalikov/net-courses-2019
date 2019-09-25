using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;

namespace TradingApp.Core.EntityInitializers
{
    public class StockContextInitializer
    {
        private Random r = new Random();
        
        public List<StockEntity>  ContextInitializer()
        {
            List<StockEntity> Stocks = new List<StockEntity>();
            for (int i = 1; i < 11; i++)
            {
                StockEntity stock = new StockEntity();
                stock.Type = $"Stock{i}";
                stock.Price = 250 * i + r.Next(100);
                Stocks.Add(stock);
            }
            return Stocks;
        }
    }
}
