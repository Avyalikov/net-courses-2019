using stockSimulator.Core.DTO;
using stockSimulator.Core.Services;
using stockSimulator.Modulation.Dependencies;
using StructureMap;
using System;
using System.Linq;
using System.Timers;

namespace stockSimulator.Modulation
{
    class Simutator
    {
        private static Timer Timer;
        private static StockSimulatorDbContext db;
        private int period;
        private bool dbInitialize;

        public Simutator(int period, bool dbInitialize)
        {
            this.period = period;
            this.dbInitialize = dbInitialize;
        }

        internal void start()
        {
            Logger.InitLogger();
            try
            {
                db = new StockSimulatorDbContext();
            }
            catch(Exception ex)
            {
                Logger.Log.Error("Unable to connect to Database! Error: " + ex.Message);
            }

            DbInitialize(dbInitialize);
            SetTimer(period);
            Logger.Log.Info($@"Connection to Database was created. 
Database: {db.Database.Connection.ConnectionString} 
DbRecreation: { dbInitialize} 
Interval between trading: {period} ms.");

            UserInterface ui = new UserInterface();
            ui.start();
        }

        internal void stop()
        {
            Timer.Stop();
            Timer.Dispose();
            db.Database.Connection.Close();
            Logger.Log.Info($@"Connection to Database was closed: {db.Database.Connection.State}.
Timer was stopped and disposed.");
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("At {0:HH:mm:ss.fff}", e.SignalTime);
            DoTrade();
        }

        private static void DoTrade()
        {
            Random random = new Random();
            int numberOfCustomer;
            int numberOfSeller;
            var container = new Container(new StockSimulatorRegistry());

            var clientService = container.GetInstance<ClientService>();
            var transactionService = container.GetInstance<TransactionService>();
            int numberOfClients = db.Clients.Count();
            if (numberOfClients < 2)
            {
                throw new ArgumentException("There is less than 2 Clients, check your DataBase or collection");
            }

            GetTwoClients(numberOfClients, out numberOfCustomer, out numberOfSeller);
            var customer = clientService.GetClient(numberOfCustomer);
            var seller = clientService.GetClient(numberOfSeller);
            int sellersTypesOfStocks = seller.Stocks.Count();
            Logger.Log.Info($"{customer.Name} {customer.Surname} wants to buy some stocks.");
            if (sellersTypesOfStocks == 0)
            {
                Logger.Log.Info($"But {seller.Name} {seller.Surname} has no stocks to sell.");
               // Console.WriteLine($"{seller.Name} has no stocks to sell");
                return;
            }

            int wantedTypeStock = GetRandomNumberFromRange(sellersTypesOfStocks);
            var wantedStock = seller.Stocks.ElementAt(wantedTypeStock - 1);
            int numberOfAvailableSellerStock = wantedStock.Amount;
            int numberOfWantedStocks = GetRandomNumberFromRange(numberOfAvailableSellerStock);
            Logger.Log.Info($"{customer.Name} {customer.Surname} wants to buy {numberOfWantedStocks} stock(s) of {wantedStock.Stock.Name}\n" +
                $"and {seller.Name} {seller.Surname} has {numberOfAvailableSellerStock} stock(s) of {wantedStock.Stock.Name}.\n" +
                $"This deal cost {wantedStock.Stock.Cost * numberOfWantedStocks}. {customer.Name} {customer.Surname} has {customer.AccountBalance} money.\n" +
                $"And {seller.Name} {seller.Surname} has {seller.AccountBalance} money.");
            TradeInfo tradeInfo = new TradeInfo
            {
                Amount = numberOfWantedStocks,
                Customer_ID = numberOfCustomer,
                Seller_ID = numberOfSeller,
                Stock_ID = wantedStock.StockID
            };

            transactionService.Trade(tradeInfo);
           // Console.WriteLine($"Between {customer.Name} and {seller.Name} was transaction on {numberOfWantedStocks} stock(s) of '{wantedStock.Stock.Name}'." 
             //   + Environment.NewLine);
        }

        private static int GetRandomNumberFromRange(int maxValue)
        {
            if (maxValue == 1)
            {
                return maxValue;
            }

            Random random = new Random();

            int value = random.Next(1, maxValue);
            return value;
        }

        private static void GetTwoClients(int numberOfClients, out int numberOfCustomer, out int numberOfSeller)
        {
            Random random = new Random();
            numberOfCustomer = random.Next(1, numberOfClients + 1);
            do
            {
                numberOfSeller = random.Next(1, numberOfClients + 1);
            }
            while (numberOfCustomer == numberOfSeller);
        }

        private static void SetTimer(int period)
        {
            // Create a timer with a two second interval.
            Timer = new Timer(period);
            // Hook up the Elapsed event for the timer. 
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private static void DbInitialize(bool recreate = false)
        {
           db.Database.Initialize(recreate);
        }
    }
}
