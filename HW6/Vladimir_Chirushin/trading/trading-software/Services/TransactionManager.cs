using System;
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
        public TransactionManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            IClientManager clientManager,
            IStockManager stockManager,
            ITableDrawer tableDrawer,
            IBlockOfSharesManager blockOfSharesManager)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.tableDrawer = tableDrawer;
            this.blockOfSharesManager = blockOfSharesManager;
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
            using (var db = new TradingContext())
            {
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
            }
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

            using (var db = new TradingContext())
            {
                int StockID = db.Stocks
                           .Where(s => s.StockType == stocksInput)
                           .FirstOrDefault<Stock>().StockID;

                int SellerID = db.Clients
                                   .Where(c => c.Name == sellerInput)
                                   .FirstOrDefault<Client>().ClientID;
                int BuyerID = db.Clients
                                   .Where(c => c.Name == buyerInput)
                                   .FirstOrDefault<Client>().ClientID;
                Transaction transaction = new Transaction { dateTime = DateTime.Now, SellerID = SellerID, BuyerID = BuyerID, StockID = StockID, Amount = stockAmount };
                if (Validate(transaction))
                {
                    AddTransaction(transaction);
                }
            }
        }


        public void ReadAllTransactions()
        {
            using (var db = new TradingContext())
            {
                IQueryable<Transaction> query = db.TransactionHistory.AsQueryable<Transaction>();
                tableDrawer.Show(query);
            }
        }


        private  bool Validate(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                bool IsSellerHasEnoughStocks;
                BlockOfShares SellerBlock = db.BlockOfSharesTable.Where(s => s.ClientID == transaction.SellerID &&
                                              s.StockID == transaction.StockID).FirstOrDefault();
                if (SellerBlock != null)
                {
                    IsSellerHasEnoughStocks = SellerBlock.Amount >= transaction.Amount ? true : false;
                }
                else
                {
                    IsSellerHasEnoughStocks = false;
                }
                bool IsSellerAndBuyerDifferent =
                    transaction.SellerID != transaction.BuyerID;

                bool IsBuyerCanAffordStocks =
                    db.Clients.Where(c => c.ClientID == transaction.BuyerID)
                    .FirstOrDefault().Balance > 0;
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
        }
        private void TransactionAgent(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                Client BuyerClient = db.Clients
                    .Where(c => c.ClientID == transaction.BuyerID)
                    .FirstOrDefault();

                Client SellerClient = db.Clients
                    .Where(c => c.ClientID == transaction.SellerID)
                    .FirstOrDefault();

                BlockOfShares sellerBlockOfShare = new BlockOfShares
                {
                    ClientID = SellerClient.ClientID,
                    StockID = transaction.StockID,
                    Amount = (-1) * transaction.Amount // subtract stock from seller
                };

                BlockOfShares BuyerBlockOfShare = new BlockOfShares
                {
                    ClientID = BuyerClient.ClientID,
                    StockID = transaction.StockID,
                    Amount = transaction.Amount
                };

                blockOfSharesManager.AddShare(sellerBlockOfShare);
                blockOfSharesManager.AddShare(BuyerBlockOfShare);

                Stock stock = db.Stocks.Where(s => s.StockID == transaction.StockID).FirstOrDefault();

                clientManager.ChangeBalance(BuyerClient.ClientID, -1 * stock.Price * transaction.Amount);
                clientManager.ChangeBalance(SellerClient.ClientID, stock.Price * transaction.Amount);
            }
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
                IQueryable<Transaction> query = new[] { transaction }.AsQueryable();
                tableDrawer.Show(query);
                outputDevice.WriteLine("Failed to create transaction:");
                return false;
            }
        }
    }
}
