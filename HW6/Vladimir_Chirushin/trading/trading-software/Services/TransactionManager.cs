using System;
using System.Linq;
using System.Timers;

namespace trading_software
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly ITableDrawer tableDrawer;
        private readonly ITransactionValidator transactionValidator;
        public TransactionManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            IClientManager clientManager,
            IStockManager stockManager,
            ITableDrawer tableDrawer,
            ITransactionValidator transactionValidator)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.tableDrawer = tableDrawer;
            this.transactionValidator = transactionValidator;
        }

        public void AddTransaction(Client sellerClient, Client buyerClient, Stock stock, int stockAmount)
        {
            using (var db = new TradingContext())
            {
                var transaction = new Transaction
                {
                    dateTime = DateTime.Now,
                    Seller = sellerClient,
                    Buyer = buyerClient,
                    Stocks = stock,
                    Amount = stockAmount
                };
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
            }
        }
        public void AddNewTransaction()
        {

            using (var db = new TradingContext())
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
                var stock = db.Stocks
                           .Where(s => s.StockType == stocksInput)
                           .FirstOrDefault<Stock>();

                var sellerClient = db.Clients
                                   .Where(c => c.Name == sellerInput)
                                   .FirstOrDefault<Client>();
                var buyerClient = db.Clients
                                   .Where(c => c.Name == buyerInput)
                                   .FirstOrDefault<Client>();
                Transaction transaction = new Transaction { dateTime = DateTime.Now, Seller = sellerClient, Buyer = buyerClient, Stocks = stock, Amount = stockAmount };
                if (transactionValidator.Validate(transaction))
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

        public bool MakeRandomTransaction()
        {
            Random random = new Random();
            using (var db = new TradingContext())
            {
                const int stockAmountMax = 15;
                int stockAmount = random.Next(1, stockAmountMax);

                Transaction transaction =
                    new Transaction
                    {
                        dateTime = DateTime.Now,
                        Seller = clientManager.SelectRandom(),
                        Buyer = clientManager.SelectRandom(),
                        Stocks = stockManager.SelectRandom(),
                        Amount = stockAmount
                    };
                //if (transactionValidator.Validate(transaction))
                //{
                    AddTransaction(transaction);
                    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
        }
    }
}
