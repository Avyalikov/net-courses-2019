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
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public TradingEngine(
            IOutputDevice outputDevice,
            IInputDevice inputDevice,
            ITableDrawer tableDrawer,
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager
            )
        {
            this.outputDevice = outputDevice;
            this.inputDevice = inputDevice;
            this.tableDrawer = tableDrawer;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.transactionManager = transactionManager;
            this.blockOfSharesManager = blockOfSharesManager;
        }


        


        public void Run()
        {
            DataBaseInitializer dbInitializer = new DataBaseInitializer(clientManager, stockManager, blockOfSharesManager);
            

            Timer aTimer = new Timer(100);
            void SetTimer()
            {
                if (!aTimer.Enabled)
                {
                    aTimer.Elapsed += ATimer_Elapsed;
                    aTimer.AutoReset = true;
                    aTimer.Enabled = true;
                }
            }

            void ResetTimer()
            {
                if (aTimer.Enabled)
                {
                    aTimer.Elapsed -= ATimer_Elapsed;
                    aTimer.AutoReset = true;
                    aTimer.Enabled = false;
                }
            }

            void ATimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                if (transactionManager.MakeRandomTransaction())
                {
                    outputDevice.WriteLine("New random transaction added!");
                }
            }


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
                        blockOfSharesManager.AddNewShares();
                        break;
                    case ConsoleKey.D8:
                        blockOfSharesManager.ShowAllShares();
                        break;
                    case ConsoleKey.D0:
                        transactionManager.MakeRandomTransaction();
                        break;
                    case ConsoleKey.T:
                        SetTimer();
                        break;
                    case ConsoleKey.R:
                        aTimer.Stop();
                        aTimer.Dispose();
                        break;
                    case ConsoleKey.I:
                        dbInitializer.Initiate();
                        break;
                    case ConsoleKey.M:
                        {
                            Random random = new Random();
                            int maxAmountOfShares = 16;
                            int numberOfShares = 200;

                            Client client;
                            Stock stock;
                            for (int i = 0; i < numberOfShares; i++)
                            {
                                client = clientManager.SelectRandom();
                                stock = stockManager.SelectRandom();
                                BlockOfShares blockOfShares = new BlockOfShares
                                {
                                    ClienInBLock = client,
                                    StockInBlock = stock,
                                    NumberOfShares = random.Next(maxAmountOfShares)
                                };
                                Console.WriteLine($"Client ID:{client.ClientID}");
                                Console.WriteLine($"Stock ID:{stock.StockID}");

                                blockOfSharesManager.AddShare(blockOfShares);
                            }
                        }
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
