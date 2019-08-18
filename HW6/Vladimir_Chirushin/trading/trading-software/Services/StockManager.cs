namespace trading_software
{
    using System;
    public class StockManager : IStockManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IDataBaseDevice dataBaseDevice;
        Random random = new Random();

        public StockManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            ITableDrawer tableDrawer,
            IDataBaseDevice dataBaseDevice
            )
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
            this.dataBaseDevice = dataBaseDevice;
        }


        bool IsExist(Stock stock)
        {
            return dataBaseDevice.IsStockExist(stock.StockType);
        }

        public int SelectRandomID()
        {
            int numberOfStocks = dataBaseDevice.GetNumberOfStocks();
            int stockID = random.Next(1, numberOfStocks);
            return stockID;
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
            if (!IsExist(stock))
            {
                dataBaseDevice.Add(stock);
            }
            else
            {
                outputDevice.WriteLine("Stock already exist!");
            }
        }
        public void ManualAddStock()
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


        public void ReadAllStocks()
        {
            var stocks = dataBaseDevice.GetAllStocks();
            tableDrawer.Show(stocks);
        }
    }
}
