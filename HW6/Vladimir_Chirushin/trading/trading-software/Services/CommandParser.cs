using System;

namespace trading_software
{
    public enum Command {
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
        StartSimlationWithRandomTransactions,
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

        public CommandParser(
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager,
            IDataBaseInitializer dbInitializer,
            IOutputDevice outputDevice,
            ITimeManager timeManager
            )
        {
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.transactionManager = transactionManager;
            this.blockOfSharesManager = blockOfSharesManager;
            this.dbInitializer = dbInitializer;
            this.outputDevice = outputDevice;
            this.timeManager = timeManager;
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
                case Command.ManualAddClient:
                    clientManager.ManualAddClient();
                    break;

                case Command.ReadAllClients:
                    clientManager.ReadAllClients();
                    break;

                case Command.ManualAddStock:
                    stockManager.ManualAddStock();
                    break;

                case Command.ReadAllStocks:
                    stockManager.ReadAllStocks();
                    break;
                case Command.ManualAddTransaction:
                    transactionManager.ManualAddTransaction();
                    break;

                case Command.ReadAllTransactions:
                    transactionManager.ReadAllTransactions();
                    break;

                case Command.ManualAddShares:
                    blockOfSharesManager.ManualAddNewShare();
                    break;

                case Command.ReadAllShares:
                    blockOfSharesManager.ShowAllShares();
                    break;

                case Command.MakeRandomTransaction:
                    transactionManager.MakeRandomTransaction();
                    break;

                case Command.StartSimlationWithRandomTransactions:
                    timeManager.StartRandomTransactionThread();
                break;

                case Command.StopSimulationWithRandomTransactions:
                    timeManager.StopRandomTransactionThread();
                    break;

                case Command.InitiateDB:
                    dbInitializer.Initiate();
                    break;

                case Command.BankruptRandomClient:
                    clientManager.BankruptRandomClient();
                    break;

                case Command.ShowOrangeClients:
                    clientManager.ShowOrangeZone();
                    break;

                case Command.ShowBlackClients:
                    clientManager.ShowBlackClients();
                    break;

                case Command.ReduceAssetsRandomClient:
                    clientManager.ReduceAssetsRandomClient();
                    break;

                //case ConsoleKey.Escape:
                  //  continue;

               // default:
                   // ShowMenu();
                   // continue;
            }
        }
    }
}
