namespace trading_software
{
    using System.Linq;
    public class StockManager : IStockManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
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

        public void AddNewStock()
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

                var stock = new Stock
                {
                    StockType = stockName,
                    Price = stockPrice
                };
                db.Stocks.Add(stock);
                db.SaveChanges();
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
