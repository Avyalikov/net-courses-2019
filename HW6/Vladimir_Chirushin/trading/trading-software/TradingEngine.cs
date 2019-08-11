using System;
using System.Linq;
using System.Timers;
using trading_software.Services;

using trading_software;

namespace trading_software
{
    public class TradingEngine
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly ITransactionManager transactionManager;

        public TradingEngine(
            IOutputDevice outputDevice,
            IInputDevice inputDevice,
            ITableDrawer tableDrawer,
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager
            )
        {
            this.outputDevice = outputDevice;
            this.inputDevice = inputDevice;
            this.tableDrawer = tableDrawer;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.transactionManager = transactionManager;
        }


        private static Timer aTimer;
        private static void SetTimer()
        {
            // Create a timer with a ten second interval.
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += MakeRandomTransaction;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void Run()
        {
            

            ConsoleKeyInfo consoleKeyPressed;
            void ShowMenu()
            {
                outputDevice.WriteLine(@"1 - Add client
2 - Show all clients
3 - Add stock
4 - Show all stocks
5 - Add transaction
6 - show all transactions
7 - Create random transaction");

            }


            transactionManager.ReadAllTransactions();
            do
            {
                consoleKeyPressed = inputDevice.ReadKey();

                switch (consoleKeyPressed.Key)
                {
                    case ConsoleKey.D1:
                        clientManager.AddNewClient();
                        break;

                    case ConsoleKey.D2:
                        clientManager.ReadAllClients();
                        break;

                    case ConsoleKey.D3:
                        stockManager.AddNewStock();
                        break;

                    case ConsoleKey.D4:
                        stockManager.ReadAllStocks();
                        break;
                    case ConsoleKey.D5:
                        transactionManager.AddNewTransaction();
                        break;

                    case ConsoleKey.D6:
                        transactionManager.ReadAllTransactions();
                        break;

                    case ConsoleKey.D7:
                        transactionManager.MakeRandomTransaction(null, null);
                        break;
                    case ConsoleKey.T:
                        SetTimer();
                        outputDevice.WriteLine("\nPress the Enter key to exit the application...\n");
                        outputDevice.WriteLine($"The application started at {DateTime.Now}");
                        inputDevice.ReadLine();
                        aTimer.Stop();
                        aTimer.Dispose();
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
