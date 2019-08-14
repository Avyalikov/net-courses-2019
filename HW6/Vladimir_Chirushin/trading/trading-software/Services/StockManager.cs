namespace trading_software
{
    using System;
    using System.Linq;
    public class StockManager : IStockManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        Random random = new Random();

        public StockManager(
            IInputDevice inputDevice, 
            IOutputDevice outputDevice,
            ITableDrawer tableDrawer
            )
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
        }


        bool IsExist(Stock stock)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockType == stock.StockType).FirstOrDefault() != null;
            }
        }
        public int SelectRandomID()
        {
            using (var db = new TradingContext())
            {
                int numberOfStocks = db.Stocks.Count();
                int stockID = random.Next(1, numberOfStocks);
                return stockID;
            }
        }
        public void AddStock(string stockName, decimal stockPrice)
        {
            var stock = new Stock
            {
                StockType = stockName,
                Price = stockPrice
            };
            AddStock(stock);
        }
        public void AddStock(Stock stock)
        {
            using (var db = new TradingContext())
            {
                if (!IsExist(stock))
                {
                    db.Stocks.Add(stock);
                    db.SaveChanges();
                }
                else
                {
                    outputDevice.WriteLine("Stock already exist!");
                }
            }
        }
        public void ManualAddStock()
        {
            using (var db = new TradingContext())
            {
                outputDevice.WriteLine("Write Stock Type:");
                string stockName = inputDevice.ReadLine();

                outputDevice.WriteLine("Write stock price:");
                decimal stockPrice = 0;
                while (true)
                {
                    if (decimal.TryParse(inputDevice.ReadLine(), out stockPrice))
                        break;
                    else
                        outputDevice.WriteLine("Please enter valid balance");
                }
                AddStock(stockName, stockPrice);
            }
        }


        public void ReadAllStocks()
        {
            using (var db = new TradingContext())
            {
                IQueryable<Stock> query = db.Stocks.AsQueryable<Stock>();
                tableDrawer.Show(query);
            }
        }
    }
}
