using System;
using trading_software.Services;

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
        //private readonly ILoggerService loggerService;

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
            //ILoggerService loggerService
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
            //this.loggerService = loggerService;
        }


        public void Run()
        {
            
            string commandString;
            commandParser.ShowMenu();
            do
            {
                commandString = inputDevice.ReadLine();
                commandParser.Parse(commandString);
            }
            while (commandString.ToLower() != "quit");
        }
    }
}
