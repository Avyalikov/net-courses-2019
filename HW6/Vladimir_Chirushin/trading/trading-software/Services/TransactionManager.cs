using System;
using System.Collections.Generic;
using System.Linq;

namespace trading_software
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly ITableDrawer tableDrawer;
        private readonly IBlockOfSharesManager blockOfSharesManager;
        private readonly IDataBaseDevice dataBaseDevice;
        public TransactionManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            IClientManager clientManager,
            IStockManager stockManager,
            ITableDrawer tableDrawer,
            IBlockOfSharesManager blockOfSharesManager,
            IDataBaseDevice dataBaseDevice)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.tableDrawer = tableDrawer;
            this.blockOfSharesManager = blockOfSharesManager;
            this.dataBaseDevice = dataBaseDevice;
        }

        public void AddTransaction(int SellerID, int BuyerID, int StockID, int stockAmount)
        {
            var transaction = new Transaction
            {
                dateTime = DateTime.Now,
                SellerID = SellerID,
                BuyerID = BuyerID,
                StockID = StockID,
                Amount = stockAmount
            };
            AddTransaction(transaction);
        }

        public void AddTransaction(Transaction transaction)
        {
            dataBaseDevice.Add(transaction);
        }
        public void ManualAddTransaction()
        {
            outputDevice.Clear();
            clientManager.ReadAllClients();
            outputDevice.WriteLine("Select seller:");
            string sellerInput = inputDevice.ReadLine();

            outputDevice.Clear();
            clientManager.ReadAllClients();
            outputDevice.WriteLine("Select buyer:");
            string buyerInput = inputDevice.ReadLine();

            outputDevice.Clear();
            stockManager.ReadAllStocks();
            outputDevice.WriteLine("Select stock:");
            string stocksInput = inputDevice.ReadLine();

            outputDevice.WriteLine("Write stock amount:");
            int stockAmount = 0;
            while (true)
            {
                if (int.TryParse(inputDevice.ReadLine(), out stockAmount))
                    break;
                else
                    outputDevice.WriteLine("Please enter valid balance");
            }

            int StockID = dataBaseDevice.GetStockID(stocksInput);

            int SellerID = dataBaseDevice.GetClientID(sellerInput);
            int BuyerID = dataBaseDevice.GetClientID(buyerInput);

            Transaction transaction = new Transaction { dateTime = DateTime.Now, SellerID = SellerID, BuyerID = BuyerID, StockID = StockID, Amount = stockAmount };
            if (Validate(transaction))
            {
                AddTransaction(transaction);
            }
        }


        public void ReadAllTransactions()
        {
            tableDrawer.Show(dataBaseDevice.GetAllTransaction());
        }


        private bool Validate(Transaction transaction)
        {
            bool IsSellerAndBuyerDifferent = transaction.SellerID != transaction.BuyerID;

            bool IsSellerHasEnoughStocks;
            if (dataBaseDevice.IsClientHasStockType(transaction.SellerID, transaction.StockID))
            {
                int SellerStockAmount = dataBaseDevice.GetClientStockAmount(transaction.SellerID, transaction.StockID);
                IsSellerHasEnoughStocks = SellerStockAmount >= transaction.Amount ? true : false;
            }
            else
            {
                IsSellerHasEnoughStocks = false;
            }

            bool IsBuyerCanAffordStocks = dataBaseDevice.GetClientBalance(transaction.BuyerID) > 0;

            if (IsSellerAndBuyerDifferent &&
                IsSellerHasEnoughStocks &&
                IsBuyerCanAffordStocks)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TransactionAgent(Transaction transaction)
        {
            BlockOfShares sellerBlockOfShare = new BlockOfShares
            {
                ClientID = transaction.SellerID,
                StockID = transaction.StockID,
                Amount = (-1) * transaction.Amount // subtract stock from seller
            };

            BlockOfShares BuyerBlockOfShare = new BlockOfShares
            {
                ClientID = transaction.BuyerID,
                StockID = transaction.StockID,
                Amount = transaction.Amount
            };

            blockOfSharesManager.AddShare(sellerBlockOfShare);
            blockOfSharesManager.AddShare(BuyerBlockOfShare);

            decimal stockPrice = dataBaseDevice.GetStockPrice(transaction.StockID);

            clientManager.ChangeBalance(transaction.BuyerID, -1 * stockPrice * transaction.Amount);
            clientManager.ChangeBalance(transaction.SellerID, stockPrice * transaction.Amount);
        }

        public bool MakeRandomTransaction()
        {
            Random random = new Random();

            const int stockAmountMax = 15;
            int stockAmount = random.Next(1, stockAmountMax);

            Transaction transaction =
                new Transaction
                {
                    dateTime = DateTime.Now,
                    SellerID = clientManager.SelectRandomID(),
                    BuyerID = clientManager.SelectRandomID(),
                    StockID = stockManager.SelectRandomID(),
                    Amount = stockAmount
                };
            if (Validate(transaction))
            {
                TransactionAgent(transaction);
                AddTransaction(transaction);
                return true;
            }
            else
            {
                outputDevice.WriteLine("Failed to create transaction:");
                IEnumerable<Transaction> query = new[] { transaction }.AsEnumerable<Transaction>();
                tableDrawer.Show(query);
                return false;
            }
        }
    }
}
