using System;

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
        private readonly ICommandParser commandParser;

        public TradingEngine(
            IOutputDevice outputDevice,
            IInputDevice inputDevice,
            ITableDrawer tableDrawer,
            IClientManager clientManager,
            IStockManager stockManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager,
            IDataBaseInitializer dbInitializer,
            ICommandParser commandParser
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
            this.commandParser = commandParser;
        }


        public void Run()
        {
            string commandString;
            ShowMenu();
            do
            {
                commandString = inputDevice.ReadLine();
                commandParser.Parse(commandString);
            }
            while (commandString.ToLower() != "quit");
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
            foreach(Command command in Enum.GetValues(typeof(Command)))
            {
                outputDevice.WriteLine(command.ToString());
            }
        }
    }
}
