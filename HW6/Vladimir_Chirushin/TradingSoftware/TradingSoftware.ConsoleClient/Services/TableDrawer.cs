namespace TradingSoftware.ConsoleClient.Services
{
    using System.Collections.Generic;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class TableDrawer : ITableDrawer
    {
        private readonly IOutputDevice outputDevice;
        private readonly IBlockOfSharesManager blockOfSharesManager;
        private readonly IClientManager clientManager;
        private readonly IShareManager shareManager;

        public TableDrawer(
            IOutputDevice outputDevice,
            IBlockOfSharesManager blockOfSharesManager,
            IClientManager clientManager,
            IShareManager shareManager)
        {
            this.outputDevice = outputDevice;
            this.blockOfSharesManager = blockOfSharesManager;
            this.clientManager = clientManager;
            this.shareManager = shareManager;
        }

        public void Show(IEnumerable<Share> shares)
        {
            string numberColumnName = "#";
            string shareColumnName = "Share Type";
            string priceColumnName = "Price ATM";

            this.outputDevice.WriteLine($"____________________________________________");
            this.outputDevice.WriteLine($"|{numberColumnName,4}|{shareColumnName,22}|{priceColumnName,14}|");
            this.outputDevice.WriteLine($"|----|----------------------|--------------|");
            foreach (var share in shares)
            {
                this.outputDevice.WriteLine($"|{share.ShareID,4}|{share.ShareType,22}|{share.Price,14}|");
            }

            this.outputDevice.WriteLine($"|____|______________________|______________|");
        }

        public void Show(IEnumerable<Client> clients)
        {
            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            this.outputDevice.WriteLine($"___________________________________________________________");
            this.outputDevice.WriteLine($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            this.outputDevice.WriteLine($"|----|----------------------|--------------|--------------|");
            foreach (var client in clients)
            {
                this.outputDevice.WriteLine($"|{client.ClientID,4}|{client.Name,22}|{client.PhoneNumber,14}|{client.Balance,14}|");
            }

            this.outputDevice.WriteLine($"|____|______________________|______________|______________|");
        }

        public void Show(IEnumerable<Transaction> transactions)
        {
            string numberNameColumn = "#";
            string dateTimeNameColumn = "Date and Time";
            string sellerNameColumn = "Seller";
            string buyerNameColumn = "Buyer";
            string shareNameColumn = "Share";
            string amountNameColumn = "Quan";
            string transactionAmountname = "Transaction";

            this.outputDevice.WriteLine($"_________________________________________________________________________________________________________________");
            this.outputDevice.WriteLine($"|{numberNameColumn,4}|{dateTimeNameColumn,20}|{sellerNameColumn,22}|{buyerNameColumn,22}|{shareNameColumn,22}|{amountNameColumn,4}|{transactionAmountname,11}|");
            this.outputDevice.WriteLine($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            int transactionID;
            string sellerName;
            string buyerName;
            string shareName;
            decimal sharePrice;

            foreach (var transaction in transactions)
            {
                transactionID = transaction.TransactionID;
                sellerName = this.clientManager.GetClientName(transaction.SellerID);
                buyerName = this.clientManager.GetClientName(transaction.BuyerID);
                shareName = this.shareManager.GetShareType(transaction.ShareID);
                sharePrice = this.shareManager.GetSharePrice(transaction.ShareID);
                this.outputDevice.WriteLine($"|{transactionID,4}|{transaction.dateTime,20}|{sellerName,22}|{buyerName,22}|{shareName,22}|{transaction.Amount,4}|{transaction.Amount * sharePrice,10}$|");
            }

            this.outputDevice.WriteLine($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }

        public void Show(IEnumerable<BlockOfShares> blockOfShares)
        {
            string numberName = "#";
            string clientNameColumn = "Client";
            string stockNameColumn = "Stock";
            string amountName = "Amount";

            int i = 0;
            this.outputDevice.WriteLine($"___________________________________________________________");
            this.outputDevice.WriteLine($"|{numberName,4}|{clientNameColumn,22}|{stockNameColumn,22}|{amountName,6}|");
            this.outputDevice.WriteLine($"|----|----------------------|----------------------|------|");

            string clientName;
            string shareName;

            foreach (var block in blockOfShares)
            {
                i++;
                clientName = this.clientManager.GetClientName(block.ClientID);
                shareName = this.shareManager.GetShareType(block.ShareID);
                this.outputDevice.WriteLine($"|{i,4}|{clientName,22}|{shareName,22}|{block.Amount,6}|");
            }

            this.outputDevice.WriteLine($"|____|______________________|______________________|______|");
        }
    }
}