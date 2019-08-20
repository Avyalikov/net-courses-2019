namespace trading_software
{
    using System.Collections.Generic;

    public class TableDrawer : ITableDrawer
    {
        private readonly IOutputDevice outputDevice;
        private readonly IBlockOfSharesRepository blockOfSharesRepository;
        private readonly IClientRepository clientRepository;
        private readonly IStockRepository stockRepository;

        public TableDrawer(
            IOutputDevice outputDevice, 
            IBlockOfSharesRepository blockOfSharesRepository,
            IClientRepository clientRepository,
            IStockRepository stockRepository)
        {
            this.outputDevice = outputDevice;
            this.blockOfSharesRepository = blockOfSharesRepository;
            this.clientRepository = clientRepository;
            this.stockRepository = stockRepository;
        }

        public void Show(IEnumerable<Stock> Stocks)
        {
            string numberColumnName = "#";
            string stockColumnName = "Stock Type";
            string priceColumnName = "Price ATM";

            outputDevice.WriteLine($"____________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{stockColumnName,22}|{priceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|");
            foreach (var stock in Stocks)
            {
                outputDevice.WriteLine($"|{stock.StockID,4}|{stock.StockType,22}|{stock.Price,14}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________|");
        }

        public void Show(IEnumerable<Client> Clients)
        {
            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            outputDevice.WriteLine($"___________________________________________________________");
            outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var client in Clients)
            {
                outputDevice.WriteLine($"|{client.ClientID,4}|{client.Name,22}|{client.PhoneNumber,14}|{client.Balance,14}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________|______________|");
        }

        public void Show(IEnumerable<Transaction> Transactions)
        {

            string numberName = "#";
            string dateTimeName = "Date and Time";
            string sellerName = "Seller";
            string buyerName = "Buyer";
            string stockName = "Stock";
            string amountName = "Quan";
            string transactionAmountname = "Transaction";

            outputDevice.WriteLine($"_________________________________________________________________________________________________________________");
            outputDevice.WriteLine($"|{numberName,4}|{dateTimeName,20}|{sellerName,22}|{buyerName,22}|{stockName,22}|{amountName,4}|{transactionAmountname,11}|");
            outputDevice.WriteLine($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            int transactionID;
            string SellerName;
            string BuyerName;
            string StockName;
            decimal StockPrice;

            foreach (var transaction in Transactions)
            {
                transactionID = transaction.TransactionID;
                SellerName = clientRepository.GetClientName(transaction.SellerID);
                BuyerName = clientRepository.GetClientName(transaction.BuyerID);
                StockName = stockRepository.GetStockType(transaction.StockID);
                StockPrice = stockRepository.GetStockPrice(transaction.StockID);
                outputDevice.WriteLine($"|{transactionID,4}|{transaction.dateTime,20}|{SellerName,22}|{BuyerName,22}|{StockName,22}|{transaction.Amount,4}|{transaction.Amount * StockPrice,10}$|");
            }
            outputDevice.WriteLine($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }

        public void Show(IEnumerable<BlockOfShares> blockOfShares)
        {
            string numberName = "#";
            string clientName = "Client";
            string stockName = "Stock";
            string amountName = "Amount";

            int i = 0;
            outputDevice.WriteLine($"___________________________________________________________");
            outputDevice.WriteLine($"|{numberName,4}|{clientName,22}|{stockName,22}|{amountName,6}|");
            outputDevice.WriteLine($"|----|----------------------|----------------------|------|");

            string ClientName;
            string StockName;

            foreach (var block in blockOfShares)
            {
                i++;
                ClientName = clientRepository.GetClientName(block.ClientID);
                StockName = stockRepository.GetStockType(block.StockID);
                outputDevice.WriteLine($"|{i,4}|{ClientName,22}|{StockName,22}|{block.Amount,6}|");
            }
            outputDevice.WriteLine($"|____|______________________|______________________|______|");
        }
    }
}