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
            outputDevice.Clear();
            outputDevice.WriteLine($"____________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{stockColumnName,22}|{priceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|");
            foreach (var stock in Stocks)
            {
                outputDevice.WriteLine($"|{stock.StockID,4}|{stock.StockType,22}|{stock.Price,14}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________|");
        }

        public void Show(IQueryable<Client> Clients)
        {
            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            outputDevice.Clear();
            outputDevice.WriteLine($"___________________________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var client in Clients)
            {
                outputDevice.WriteLine($"|{client.ClientID,4}|{client.Name,22}|{client.PhoneNumber,14}|{client.Balance,14}|");
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

            outputDevice.Clear();
            outputDevice.WriteLine($"_________________________________________________________________________________________________________________");
            outputDevice.WriteLine($"|{numberName,4}|{dateTimeName,20}|{sellerName,22}|{buyerName,22}|{stockName,22}|{amountName,4}|{transactionAmountname,11}|");
            outputDevice.WriteLine($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            int transactionID;
            string SellerName;
            string BuyerName;
            string StockName;
            decimal StockPrice;
            using (var db = new TradingContext())
            {
                foreach (var transaction in Transactions)
                {
                    transactionID = transaction.TransactionID;
                    SellerName = db.Clients.Where(c => c.ClientID == transaction.SellerID).FirstOrDefault().Name;
                    BuyerName = db.Clients.Where(c => c.ClientID == transaction.BuyerID).FirstOrDefault().Name;
                    StockName = db.Stocks.Where(c => c.StockID == transaction.StockID).FirstOrDefault().StockType;
                    StockPrice = db.Stocks.Where(c => c.StockID == transaction.StockID).FirstOrDefault().Price;
                    outputDevice.WriteLine($"|{transactionID,4}|{transaction.dateTime,20}|{SellerName,22}|{BuyerName,22}|{StockName,22}|{transaction.Amount,4}|{transaction.Amount * StockPrice,10}$|");
                }
                outputDevice.WriteLine($"|____|____________________|______________________|______________________|______________________|____|___________|");
            }
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

            string ClientName;
            string StockName;
            using (var db = new TradingContext())
            {
                foreach (var block in blockOfShares)
                {
                    i++;
                    ClientName = db.Clients.Where(c => c.ClientID == block.ClientID).FirstOrDefault().Name;
                    StockName = db.Stocks.Where(c => c.StockID == block.StockID).FirstOrDefault().StockType;
                    outputDevice.WriteLine($"|{i,4}|{ClientName,22}|{StockName,22}|{block.Amount,6}|");
                }
                outputDevice.WriteLine($"|____|______________________|______________________|______|");
            }
        }
    }
}
