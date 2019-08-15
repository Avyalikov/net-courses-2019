using System;
using System.Timers;

namespace trading_software
{
    public class TradingEngine : ITradingEngine
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly ITransactionManager transactionManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;
        private readonly IDataBaseInitializer dbInitializer;

        public TradingEngine(
            IOutputDevice outputDevice,
            IInputDevice inputDevice,
            ITableDrawer tableDrawer,
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager,
            IDataBaseInitializer dbInitializer
            )
        {
            this.outputDevice = outputDevice;
            this.inputDevice = inputDevice;
            this.tableDrawer = tableDrawer;
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.transactionManager = transactionManager;
            this.blockOfSharesManager = blockOfSharesManager;
            this.dbInitializer = dbInitializer;
        }


        public void Run()
        {
            ConsoleKeyInfo consoleKeyPressed;
            ShowMenu();
            transactionManager.ReadAllTransactions();
            do
            {
                consoleKeyPressed = inputDevice.ReadKey();

                switch (consoleKeyPressed.Key)
                {
                    case ConsoleKey.D1:
                        clientManager.ManualAddClient();
                        break;

                    case ConsoleKey.D2:
                        clientManager.ReadAllClients();
                        break;

                    case ConsoleKey.D3:
                        stockManager.ManualAddStock();
                        break;

                    case ConsoleKey.D4:
                        stockManager.ReadAllStocks();
                        break;
                    case ConsoleKey.D5:
                        transactionManager.ManualAddTransaction();
                        break;

                    case ConsoleKey.D6:
                        transactionManager.ReadAllTransactions();
                        break;

                    case ConsoleKey.D7:
                        blockOfSharesManager.ManualAddNewShare();
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
                        ResetTimer();
                        break;
                    case ConsoleKey.D:
                        aTimer.Stop();
                        aTimer.Dispose();
                        break;

                    case ConsoleKey.I:
                        dbInitializer.Initiate();
                        break;

                    case ConsoleKey.M:
                        GenerateRandomBlockShares();
                        break;

                    case ConsoleKey.B:
                        clientManager.BankruptRandomClient();
                        break;

                    case ConsoleKey.Q:
                        clientManager.ShowOrangeZone();
                        break;

                    case ConsoleKey.W:
                        clientManager.ShowBlackClients();
                        break;

                    case ConsoleKey.A:
                        clientManager.ReduceAssetsRandomClient();
                        break;

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

        private void GenerateRandomBlockShares()
        {
            int numberOfShares = 200;
            for (int i = 0; i < numberOfShares; i++)
            {
                blockOfSharesManager.CreateRandomShare();
            }
        }
        private void ShowMenu()
        {
            outputDevice.WriteLine(@"MenuShowed");

        }


        Timer aTimer = new Timer(100);
        private void SetTimer()
        {
            if (!aTimer.Enabled)
            {
                aTimer.Elapsed += ATimer_Elapsed;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }
        }

        private void ResetTimer()
        {
            if (aTimer.Enabled)
            {
                aTimer.Elapsed -= ATimer_Elapsed;
                aTimer.AutoReset = true;
                aTimer.Enabled = false;
            }
        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (transactionManager.MakeRandomTransaction())
            {
                outputDevice.WriteLine("New random transaction added!");
            }
        }

    }
}
