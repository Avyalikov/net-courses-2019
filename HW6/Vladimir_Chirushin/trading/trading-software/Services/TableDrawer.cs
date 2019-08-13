using System.Linq;

namespace trading_software
{
    public class TableDrawer : ITableDrawer
    {
        private readonly IOutputDevice outputDevice;
        public TableDrawer(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }


        public void Show(IQueryable<Stock> Stocks)
        {
            string numberColumnName = "#";
            string stockColumnName = "Stock Type";
            string priceColumnName = "Price ATM";
            int i = 0;
            outputDevice.Clear();
            outputDevice.WriteLine($"____________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{stockColumnName,22}|{priceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|");
            foreach (var stock in Stocks)
            {
                i++;
                outputDevice.WriteLine($"|{i,4}|{stock.StockType,22}|{stock.Price,14}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________|");
        }

        public void Show(IQueryable<Client> Clients)
        {
            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";
            int i = 0;
            outputDevice.Clear();
            outputDevice.WriteLine($"___________________________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var client in Clients)
            {
                i++;
                outputDevice.WriteLine($"|{i,4}|{client.Name,22}|{client.PhoneNumber,14}|{client.Balance,14}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________|______________|");
        }

        public void Show(IQueryable<Transaction> Transactions)
        {
            string numberName = "#";
            string dateTimeName = "Date and Time";
            string sellerName = "Seller";
            string buyerName = "Buyer";
            string stockName = "Stock";
            string amountName = "Quan";
            string transactionAmountname = "Transaction";

            int i = 0;
            outputDevice.Clear();
            outputDevice.WriteLine($"_________________________________________________________________________________________________________________");
            outputDevice.WriteLine($"|{numberName,4}|{dateTimeName,20}|{sellerName,22}|{buyerName,22}|{stockName,22}|{amountName,4}|{transactionAmountname,11}|");
            outputDevice.WriteLine($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");


            foreach (var transaction in Transactions)
            {
                i++;
                outputDevice.WriteLine($"|{i,4}|{transaction.dateTime,20}|{transaction.Seller.Name,22}|{transaction.Buyer.Name,22}|{transaction.Stocks.StockType,22}|{transaction.Amount,4}|{transaction.Amount * transaction.Stocks.Price,10}$|");
            }
            outputDevice.WriteLine($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }

        public void Show(IQueryable<BlockOfShares> blockOfShares)
        {
            string numberName = "#";
            string clientName = "Client";
            string stockName = "Stock";
            string amountName = "Amount";

            int i = 0;
            outputDevice.Clear();
            outputDevice.WriteLine($"___________________________________________________________");
            outputDevice.WriteLine($"|{numberName,4}|{clientName,22}|{stockName,22}|{amountName,6}|");
            outputDevice.WriteLine($"|----|----------------------|----------------------|------|");


            foreach (var block in blockOfShares)
            {
                i++;
                outputDevice.WriteLine($"|{i,4}|{block.ClienInBLock.Name,22}|{block.StockInBlock.StockType,22}|{block.NumberOfShares,6}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________________|______|");
        }
    }
}
