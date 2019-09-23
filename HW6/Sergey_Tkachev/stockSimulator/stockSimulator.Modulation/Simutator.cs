namespace stockSimulator.Modulation
{
    using System;
    using System.Linq;
    using System.Timers;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Services;
    using stockSimulator.Modulation.Dependencies;
    using StructureMap;

    internal class Simutator
    {
        private static Timer timer;
        private static StockSimulatorDbContext db;
        private int period;
        private bool dbInitialize;

        /// <summary>
        /// Initializes an Instance of Simutator class.
        /// </summary>
        /// <param name="period">Period for timer in miliseconds.</param>
        /// <param name="dbInitialize">Flag for DB Initialization. True - recreates DB in any way. False - doesn't recreate Database if it was already recreated.</param>
        public Simutator(int period, bool dbInitialize)
        {
            this.period = period;
            this.dbInitialize = dbInitialize;
        }

        /// <summary>
        /// Starts simulation process by DB initialisation, setting timer and starting user interface.
        /// </summary>
        internal void Start()
        {
            DbInitialize(this.dbInitialize);
            SetTimer(this.period);
            Logger.Log.Info($@"Connection to Database was created. 
Database: {db.Database.Connection.ConnectionString} 
DbRecreation: { dbInitialize} 
Interval between trading: {period} ms.");

            UserInterface ui = new UserInterface();
            ui.Start();
        }

        /// <summary>
        /// Stops simulation by stopping and disposing timer, and closing DB connection.
        /// </summary>
        internal void Stop()
        {
            timer.Stop();
            timer.Dispose();
            db.Database.Connection.Close();
            Logger.Log.Info($@"Connection to Database was closed: {db.Database.Connection.State}.
Timer was stopped and disposed.");
        }

        /// <summary>
        /// Does trade as timer's event.
        /// </summary>
        /// <param name="source">Caller object.</param>
        /// <param name="e">Provides data for Elapsed event.</param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // Console.WriteLine("At {0:HH:mm:ss.fff}", e.SignalTime);
            DoTrade();
        }

        /// <summary>
        /// Makes trade process between 2 random clients.
        /// </summary>
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

        /// <summary>
        /// Returns random number from 1 till max value.
        /// </summary>
        /// <param name="maxValue">Max value.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Select random customer and seller from clients. 
        /// </summary>
        /// <param name="numberOfClients">Number of clients.</param>
        /// <param name="numberOfCustomer">Customer ID.</param>
        /// <param name="numberOfSeller">Seller ID.</param>
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

        /// <summary>
        /// Sets period to timer.
        /// </summary>
        /// <param name="period">Period in miliseconds.</param>
        private static void SetTimer(int period)
        {
            // Create a timer with a two second interval.
            timer = new Timer(period);

            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Itinitializes DataBase.
        /// </summary>
        /// <param name="recreate">If true initializer runs even it was runned before.</param>
        private static void DbInitialize(bool recreate = false)
        {
            Logger.InitLogger();
            try
            {
                db = new StockSimulatorDbContext();
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Unable to connect to Database! Error: " + ex.Message);
            }

            db.Database.Initialize(recreate);
        }
    }
}
