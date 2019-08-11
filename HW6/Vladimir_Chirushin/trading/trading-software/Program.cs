using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityForNorthwind.Services;
using log4net;
using log4net.Config;
namespace trading_software
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public virtual List<Stock> AvailableStocks { get; set; }
    }

    public class Stock
    {
        public int StockID { get; set; }
        public string StockType { get; set; }
        public decimal Price { get; set; }
        
    }

    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTime dateTime { get; set; }
        public virtual Client Seller { get; set; }
        public virtual Client Buyer { get; set; }
        public virtual Stock Stocks { get; set; }
        public int Amount { get; set; }
    }

    public class TradingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> TransactionHistory { get; set; }

    }


    class Program
    {
        public static void AddNewClient()
        {
            using (var db = new TradingContext())
            {
                Console.WriteLine("Write name:");
                string name = Console.ReadLine();

                Console.WriteLine("Write PhoneNumber:");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Write Balance:");
                decimal balance = 0;
                while (true)
                {
                    if (decimal.TryParse(Console.ReadLine(), out balance))
                        break;
                    else
                        Console.WriteLine("Please enter valid balance");
                }
                var client = new Client
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Balance = balance
                };
                db.Clients.Add(client);
                db.SaveChanges();
            }
        }

        public static void ReadAllClients()
        {

            using (var db = new TradingContext())
            {
                var query = from b in db.Clients
                            orderby b.Name
                            select new { b.Name, b.PhoneNumber, b.Balance };
                int i=0;
                foreach (var iteam in query)
                {
                    i++;
                    Console.WriteLine($"{i}).================================================");
                    Console.WriteLine($"We have client with name: {iteam.Name}.");
                    Console.WriteLine($"We can call him by number: {iteam.PhoneNumber}.");
                    Console.WriteLine($"He has enormous balance size of: ${iteam.Balance}");
                }
            }
        }


        public static void AddNewStock()
        {
            using (var db = new TradingContext())
            {
                Console.WriteLine("Write Stock Type:");
                string stockName = Console.ReadLine();

                Console.WriteLine("Write stock price:");
                decimal stockPrice = 0;
                while (true)
                {
                    if (decimal.TryParse(Console.ReadLine(), out stockPrice))
                        break;
                    else
                        Console.WriteLine("Please enter valid balance");
                }

                var stock = new Stock
                {
                    StockType = stockName,
                    Price = stockPrice
                };
                db.Stocks.Add(stock);
                db.SaveChanges();
            }
        }

        public static void ReadAllStocks()
        {

            using (var db = new TradingContext())
            {
                var query = from s in db.Stocks
                            orderby s.StockType
                            select new { s.StockType, s.Price };
                int i = 0;
                foreach (var iteam in query)
                {
                    i++;
                    Console.WriteLine($"{i}).================================");
                    Console.WriteLine($"We have stocks type: {iteam.StockType}.");
                    Console.WriteLine($"ATM it has price: ${iteam.Price}.");
                }
            }
        }


        public static void AddNewTransaction()
        {

            using (var db = new TradingContext())
            {
                Console.Clear();
                ReadAllClients();
                Console.WriteLine("Select seller:");
                string sellerInput = Console.ReadLine();

                Console.Clear();
                ReadAllClients();
                Console.WriteLine("Select buyer:");
                string buyerInput = Console.ReadLine();

                Console.Clear();
                ReadAllStocks();
                Console.WriteLine("Select stock:");
                string stocksInput = Console.ReadLine();

                Console.WriteLine("Write stock amount:");
                int stockAmount = 0;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out stockAmount))
                        break;
                    else
                        Console.WriteLine("Please enter valid balance");
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


        public static void ReadAllTransactions()
        {

            using (var db = new TradingContext())
            {
                var query = from t in db.TransactionHistory
                            orderby t.dateTime
                            select new { t.dateTime, t.Seller, t.Buyer, t.Stocks, t.Amount };
                int i = 0;
                foreach (var iteam in query)
                {
                    i++;
                    Console.WriteLine($"{i}).================================");
                    Console.WriteLine($"{iteam.dateTime} {iteam.Seller.Name} {iteam.Buyer.Name} {iteam.Stocks.StockType} {iteam.Amount}");
                }
            }
        }


        static void Main(string[] args)
        {

            // setup log4net:
            log4net.Config.XmlConfigurator.Configure();
            ILoggerService logger = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            void MakeRandomTransaction()
            {
                Random random = new Random();
                using (var db = new TradingContext())
                {
                    const int stockAmountMax = 15;

                    int numberOfClients = db.Clients.Count();
                    int clientId = random.Next(1, numberOfClients);
                    var sellerClient = db.Clients
                                   .FirstOrDefault<Client>(c => c.ClientID == clientId);

                    clientId = random.Next(1, numberOfClients);
                    var buyerClient = db.Clients
                                   .FirstOrDefault<Client>(c => c.ClientID == clientId);

                    int numberOfStocks = db.Stocks.Count();
                    int stockID = random.Next(1, numberOfStocks);
                    var stock = db.Stocks
                                   .FirstOrDefault<Stock>(s => s.StockID == stockID);

                    int stockAmount = random.Next(0, stockAmountMax);

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


            ConsoleKeyInfo consoleKeyPressed;
            void ShowMenu()
            {
                Console.WriteLine(@"1 - Add client
2 - Show all clients
3 - Add stock
4 - Show all stocks
5 - Add transaction
6 - show all transactions
7 - Create random transaction");

            }
            do
            {
                consoleKeyPressed = Console.ReadKey(true);

                switch (consoleKeyPressed.Key)
                {
                    case ConsoleKey.D1:
                        AddNewClient();
                        break;

                    case ConsoleKey.D2:
                        ReadAllClients();
                        break;

                    case ConsoleKey.D3:
                        AddNewStock();
                        break;

                    case ConsoleKey.D4:
                        ReadAllStocks();
                        break;
                    case ConsoleKey.D5:
                        AddNewTransaction();
                        break;

                    case ConsoleKey.D6:
                        ReadAllTransactions();
                        break;

                    case ConsoleKey.D7:
                        MakeRandomTransaction();
                        break;

                    case ConsoleKey.E:
                        break;

                    case ConsoleKey.R:
                        break;
                    case ConsoleKey.T:
                        break;

                    case ConsoleKey.Escape:
                        continue;
                    default:
                        ShowMenu();
                        continue;
                }
            }
            while (consoleKeyPressed.Key != ConsoleKey.Escape);
        }
    }
}
