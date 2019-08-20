namespace trading_software
{
    using System;
    using trading_software.Services;

    public enum Command
    {
        Help,
        ManualAddClient,
        ManualAddStock,
        ManualAddTransaction,
        ManualAddShares,
        ReadAllClients,
        ReadAllStocks,
        ReadAllTransactions,
        ReadAllShares,
        MakeRandomTransaction,
        InitiateDB,
        BankruptRandomClient,
        ShowOrangeClients,
        ShowBlackClients,
        ReduceAssetsRandomClient,
        StartSimulationWithRandomTransactions,
        StopSimulationWithRandomTransactions
    }

    public class CommandParser : ICommandParser
    {
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly ITransactionManager transactionManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;
        private readonly IDataBaseInitializer dbInitializer;
        private readonly IOutputDevice outputDevice;
        private readonly ITimeManager timeManager;
        private readonly ILoggerService loggerService;

        public CommandParser(
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager,
            IDataBaseInitializer dbInitializer,
            IOutputDevice outputDevice,
            ITimeManager timeManager,
            ILoggerService loggerService
            )
        {
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.transactionManager = transactionManager;
            this.blockOfSharesManager = blockOfSharesManager;
            this.dbInitializer = dbInitializer;
            this.outputDevice = outputDevice;
            this.timeManager = timeManager;
            this.loggerService = loggerService;
        }

        Command command;
        public void Parse(string commandString)
        {
            if (Enum.TryParse(commandString, true, out command))
            {
                ExecuteCommand(command);
            }
            else
            {
                outputDevice.WriteLine("command not recognized");
            }
        }

        private void ExecuteCommand(Command command)
        {
            switch (command)
            {
                case Command.Help:
                    ShowMenu();
                    break;

                case Command.ManualAddClient:
                    ManualAddClient();
                    break;

                case Command.ReadAllClients:
                    ReadAllClients();
                    break;

                case Command.ManualAddStock:
                    ManualAddStock();
                    break;

                case Command.ReadAllStocks:
                    ReadAllStock();
                    break;
                case Command.ManualAddTransaction:
                    ManualAddTransaction();
                    break;

                case Command.ReadAllTransactions:
                    ReadAllTransactions();
                    break;

                case Command.ManualAddShares:
                    ManualAddNewShares();
                    break;

                case Command.ReadAllShares:
                    ShowAllShares();
                    break;

                case Command.MakeRandomTransaction:
                    MakeRandomTransaction();
                    break;

                case Command.StartSimulationWithRandomTransactions:
                    StartSimulation();
                    break;

                case Command.StopSimulationWithRandomTransactions:
                    StopSimulation();
                    break;

                case Command.InitiateDB:
                    InitiateDB();
                    break;

                case Command.BankruptRandomClient:
                    BankruptRandomClient();
                    break;

                case Command.ShowOrangeClients:
                    ShowOrangeClients();
                    break;

                case Command.ShowBlackClients:
                    ShowBlackClients();
                    break;

                case Command.ReduceAssetsRandomClient:
                    ReduceAssetsRandomClient();
                    break;

                default:
                    break;
            }
        }

        public void ShowMenu()
        {
            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                outputDevice.WriteLine(command.ToString());
            }
        }

        private void ManualAddClient()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.ManualAddClient());
            loggerService.Info("Manualy added client to ClientBase");
        }

        private void ReadAllClients()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.ReadAllClients());
            loggerService.Info("Readed all Clients from ClientBase");
        }

        private void ManualAddStock()
        {
            loggerService.RunWithExceptionLogging(() => stockManager.ManualAddStock());
            loggerService.Info("Manualy Added Stock");
        }

        private void ReadAllStock()
        {
            loggerService.RunWithExceptionLogging(() => stockManager.ReadAllStocks());
            loggerService.Info("Readed all stocks from StockBase");
        }

        private void ManualAddTransaction()
        {
            loggerService.RunWithExceptionLogging(() => transactionManager.ManualAddTransaction());
            loggerService.Info("Manualy added transaction");
        }

        private void ReadAllTransactions()
        {
            loggerService.RunWithExceptionLogging(() => transactionManager.ReadAllTransactions());
            loggerService.Info("Readed all transactions from TransactionBase");
        }
        private void ManualAddNewShares()
        {
            loggerService.RunWithExceptionLogging(() => blockOfSharesManager.ManualAddNewShare());
            loggerService.Info("Manualy added Share to BlockOfSharesBase");
        }

        private void ShowAllShares()
        {
            loggerService.RunWithExceptionLogging(() => blockOfSharesManager.ShowAllShares());
            loggerService.Info("Readed all Shares from BlockOfSharesBase");
        }

        private void MakeRandomTransaction()
        {
            loggerService.RunWithExceptionLogging(() => transactionManager.MakeRandomTransaction());
            loggerService.Info("Added ranom transaction to TransactionBase");
        }

        private void StartSimulation()
        {
            loggerService.RunWithExceptionLogging(() => timeManager.StartRandomTransactionThread());
            loggerService.Info("Started simulation with random transaction adder");
        }

        private void StopSimulation()
        {
            loggerService.RunWithExceptionLogging(() => timeManager.StopRandomTransactionThread());
            loggerService.Info("Stoped simulation with random Transaction adder");
        }
        private void InitiateDB()
        {
            loggerService.RunWithExceptionLogging(() => dbInitializer.Initiate());
            loggerService.Info("DataBase was initiated with default clients, stocks and BlocksOfShares");
        }

        private void BankruptRandomClient()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.BankruptRandomClient());
            loggerService.Info("Random client was bankrupt");
        }

        private void ShowOrangeClients()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.ShowOrangeZone());
            loggerService.Info("Readed all orange clients");
        }

        private void ShowBlackClients()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.ShowBlackClients());
            loggerService.Info("Readed all black clients");
        }

        private void ReduceAssetsRandomClient()
        {
            loggerService.RunWithExceptionLogging(() => clientManager.ReduceAssetsRandomClient());
            loggerService.Info("Reduce assets for random client");
        }
    }
}