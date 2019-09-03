namespace TradingApp.Services
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Interfaces;
    using TradingApp.Core.Models;
    using System.Collections.Generic;
    class TradeTable : ITradeTable
    {
        private readonly IInputOutputModule outputDevice;
        private readonly IPortfoliosService portfoliosService;
        private readonly IClientService clientService;
        private readonly ISharesService sharesService;

        public TradeTable(
            IInputOutputModule outputDevice,
            IPortfoliosService portfoliosService,
            IClientService clientService,
            ISharesService sharesService)
        {
            this.outputDevice = outputDevice;
            this.portfoliosService = portfoliosService;
            this.clientService = clientService;
            this.sharesService = sharesService;
        }

        public void Draw(IEnumerable<Share> shares)
        {
            string numberColumnName = "#";
            string shareColumnName = "Share Type";
            string priceColumnName = "Price ATM";

            this.outputDevice.WriteOutput($"____________________________________________");
            this.outputDevice.WriteOutput($"|{numberColumnName,4}|{shareColumnName,22}|{priceColumnName,14}|");
            this.outputDevice.WriteOutput($"|----|----------------------|--------------|");
            foreach (var share in shares)
            {
                this.outputDevice.WriteOutput($"|{share.ShareID,4}|{share.ShareType,22}|{share.Price,14}|");
            }

            this.outputDevice.WriteOutput($"|____|______________________|______________|");
        }

        public void Draw(IEnumerable<Client> clients)
        {
            string numberColumnName = "#";
            string nameColumnName = "Name";
            string phoneNumberColumnName = "Phone Number";
            string balanceColumnName = "Balance";

            this.outputDevice.WriteOutput($"___________________________________________________________");
            this.outputDevice.WriteOutput($"|{numberColumnName,4}|{nameColumnName,22}|{phoneNumberColumnName,14}|{balanceColumnName,14}|");
            this.outputDevice.WriteOutput($"|----|----------------------|--------------|--------------|");
            foreach (var client in clients)
            {
                this.outputDevice.WriteOutput($"|{client.ClientID,4}|{client.Name,22}|{client.PhoneNumber,14}|{client.Balance,14}|");
            }

            this.outputDevice.WriteOutput($"|____|______________________|______________|______________|");
        }

        public void Draw(IEnumerable<Transaction> transactions)
        {
            string numberNameColumn = "#";
            string dateTimeNameColumn = "Date and Time";
            string sellerNameColumn = "Seller";
            string buyerNameColumn = "Buyer";
            string shareNameColumn = "Share";
            string amountNameColumn = "Quan";
            string transactionAmountname = "Transaction";

            this.outputDevice.WriteOutput($"_________________________________________________________________________________________________________________");
            this.outputDevice.WriteOutput($"|{numberNameColumn,4}|{dateTimeNameColumn,20}|{sellerNameColumn,22}|{buyerNameColumn,22}|{shareNameColumn,22}|{amountNameColumn,4}|{transactionAmountname,11}|");
            this.outputDevice.WriteOutput($"|----|--------------------|----------------------|----------------------|----------------------|----|-----------|");

            int transactionID;
            string sellerName;
            string buyerName;
            string shareType;
            decimal sharePrice;

            foreach (var transaction in transactions)
            {
                transactionID = transaction.TransactionID;
                sellerName = this.clientService.GetClientName(transaction.SellerID);
                buyerName = this.clientService.GetClientName(transaction.BuyerID);
                shareType = this.sharesService.GetShareType(transaction.ShareID);
                sharePrice = this.sharesService.GetSharePrice(transaction.ShareID);
                this.outputDevice.WriteOutput($"|{transactionID,4}|{transaction.Date,20}|{sellerName,22}|{buyerName,22}|{shareType,22}|{transaction.AmountOfShares,4}|{transaction.AmountOfShares * sharePrice,10}$|");
            }

            this.outputDevice.WriteOutput($"|____|____________________|______________________|______________________|______________________|____|___________|");
        }

        public void Draw(IEnumerable<ClientPortfolio> portfolios)
        {
            string numberName = "#";
            string clientNameColumn = "Client";
            string stockNameColumn = "Stock";
            string amountName = "Amount";

            int i = 0;
            this.outputDevice.WriteOutput($"___________________________________________________________");
            this.outputDevice.WriteOutput($"|{numberName,4}|{clientNameColumn,22}|{stockNameColumn,22}|{amountName,6}|");
            this.outputDevice.WriteOutput($"|----|----------------------|----------------------|------|");

            string clientName;
            string shareName;

            foreach (var port in portfolios)
            {
                i++;
                clientName = this.clientService.GetClientName(port.ClientID);
                shareName = this.sharesService.GetShareType(port.ShareID);
                this.outputDevice.WriteOutput($"|{i,4}|{clientName,22}|{shareName,22}|{port.AmountOfShares,6}|");
            }

            this.outputDevice.WriteOutput($"|____|______________________|______________________|______|");
        }
    }
}
