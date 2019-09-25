using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.Core.Models;

namespace WebApiServer
{
    class TraidingDbInitializer : DropCreateDatabaseAlways<TradingSimulatorDBContext>
    {
        protected override void Seed(TradingSimulatorDBContext context)
        {
            var stocks = new List<StockEntityDB>
            {
                new StockEntityDB { Name = "Coca-Cola", PricePerItem = 123.0M },
                new StockEntityDB { Name = "Marvel", PricePerItem = 202.0M },
                new StockEntityDB { Name = "Intel", PricePerItem = 228.0M },
                new StockEntityDB { Name = "Ashot`s barbecue", PricePerItem = 333.0M },
                new StockEntityDB { Name = "Hennesy", PricePerItem = 323.0M },
                new StockEntityDB { Name = "Honey", PricePerItem = 282.0M },
                new StockEntityDB { Name = "Adidas", PricePerItem = 111.0M },
                new StockEntityDB { Name = "Shaverma Spb", PricePerItem = 427.0M }
            };

            stocks.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();
            var traders = new List<TraderEntityDB>
            {
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Brad", Surname = "Pitt", PhoneNumber = "89990009900", Balance = 100M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Roman", Surname = "Abramovich", PhoneNumber = "87778887788", Balance = 10000M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Grzegorz", Surname = "Brzeczyszczykiewicz", PhoneNumber = "84445554455", Balance = 100M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Antonio", Surname = "Banderaz", PhoneNumber = "87775557766", Balance = 100M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "German", Surname = "Brynza", PhoneNumber = "81112223344", Balance = 10000M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Vinni", Surname = "Puh", PhoneNumber = "80000000001", Balance = 12390M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Pavel", Surname = "Durov", PhoneNumber = "81111223344", Balance = 0M },
                new TraderEntityDB { CreatedAt = DateTime.Now, Name = "Ekaterina", Surname = "Velikaya", PhoneNumber = "80023000001", Balance = -390M }
            };
            context.SaveChanges();
            traders.ForEach(s => context.Traders.Add(s));

            var tradersStock = new List<StockToTraderEntityDB>
            {
                new StockToTraderEntityDB { TraderId = 2, StockId = 1, PricePerItem = 123.0M, StockCount = 10 },
                new StockToTraderEntityDB { TraderId = 4, StockId = 2, PricePerItem = 202.0M, StockCount = 4 },
                new StockToTraderEntityDB { TraderId = 5, StockId = 3, PricePerItem = 228.0M, StockCount = 6 },
                new StockToTraderEntityDB { TraderId = 2, StockId = 4, PricePerItem = 333.0M, StockCount = 8 },
                new StockToTraderEntityDB { TraderId = 1, StockId = 5, PricePerItem = 323.0M, StockCount = 9 },
                new StockToTraderEntityDB { TraderId = 1, StockId = 6, PricePerItem = 282.0M, StockCount = 3 },
                new StockToTraderEntityDB { TraderId = 6, StockId = 7, PricePerItem = 114.0M, StockCount = 4 },
                new StockToTraderEntityDB { TraderId = 3, StockId = 8, PricePerItem = 427.0M, StockCount = 5 },
                new StockToTraderEntityDB { TraderId = 3, StockId = 1, PricePerItem = 123.0M, StockCount = 6 }
            };

            tradersStock.ForEach(s => context.TraderStocks.Add(s));
            context.SaveChanges();
            var history = new List<HistoryEntity>
            {
                new HistoryEntity { CreateAt = DateTime.Now, CustomerID = 1, SellerID = 2, StockID = 3, StockCount = 3, TotalPrice = 999M },
                new HistoryEntity { CreateAt = DateTime.Now, CustomerID = 4, SellerID = 5, StockID = 6, StockCount = 2, TotalPrice = 123M },
                new HistoryEntity { CreateAt = DateTime.Now, CustomerID = 2, SellerID = 5, StockID = 3, StockCount = 3, TotalPrice = 999M },
                new HistoryEntity { CreateAt = DateTime.Now, CustomerID = 5, SellerID = 6, StockID = 6, StockCount = 2, TotalPrice = 123M }
            };

            history.ForEach(s => context.TradeHistory.Add(s));
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
