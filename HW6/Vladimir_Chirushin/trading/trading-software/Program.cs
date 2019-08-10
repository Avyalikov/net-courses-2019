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

    public class Transactions
    {
        public int TransactionsID { get; set; }
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
        public DbSet<Transactions> TransactionHistory { get; set; }

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
                    Console.WriteLine($"{i}).================================");
                    Console.WriteLine($"We have client with name: {iteam.Name}.");
                    Console.WriteLine($"We can call him by number: {iteam.PhoneNumber}.");
                    Console.WriteLine($"He has enormous balance size of: ${iteam.Balance}");
                }

                Console.ReadKey();
            }
        }


        static void Main(string[] args)
        {

            // setup log4net:
            log4net.Config.XmlConfigurator.Configure();
            ILoggerService logger = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));
            AddNewClient();
            ReadAllClients();

        }
    }
}
