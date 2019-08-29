using stockSimulator.Core.Services;
using stockSimulator.Modulation.Dependencies;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace stockSimulator.Modulation
{
    class Program
    {
        private static Timer Timer;


        static void Main(string[] args)
        {
            const int period = 2000;
            DbInitialize(true);
            SetTimer(period);

            Console.ReadKey();
            Timer.Stop();
            Timer.Dispose();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            
            int numberOfCustomer;
            int numberOfSeller;
            using (var db = new StockSimulatorDbContext())
            {
                var container = new Container(new StockSimulatorRegistry());

                var clientService = container.GetInstance<ClientService>();
                int numberOfClients = db.Clients.Count();
                if (numberOfClients < 2)
                    throw new ArgumentException("There are less than 2 Clients, check your DataBase or variable");
                GetTwoClients(numberOfClients, out numberOfCustomer, out numberOfSeller);
                var customer = clientService.GetClient(numberOfCustomer);
                var seller = clientService.GetClient(numberOfSeller);
                int sellersStocks = seller.Stocks.Count();


            }
        }

        private static void GetTwoClients(int numberOfClients, out int numberOfCustomer, out int numberOfSeller)
        {
            Random random = new Random();
            numberOfCustomer = random.Next(0, numberOfClients);
            do
            {
                numberOfSeller = random.Next(0, numberOfClients);
            } while (numberOfCustomer == numberOfSeller);
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
            using (var db = new StockSimulatorDbContext())
            {
                db.Database.Initialize(recreate);
            }
        }
    }
}
