namespace trading_software
{
    using StructureMap;
    using trading_software.Services;

    public class TradingSoftwareRegistry : Registry
    {
        public TradingSoftwareRegistry()
        {
            this.For<ITradingEngine>().Use<TradingEngine>();

            this.For<IOutputDevice>().Use<OutputDevice>();
            this.For<IInputDevice>().Use<InputDevice>();
            this.For<IClientManager>().Use<ClientManager>();
            this.For<IStockManager>().Use<StockManager>();
            this.For<ITableDrawer>().Use<TableDrawer>();
            this.For<ITransactionManager>().Use<TransactionManager>();
            this.For<IBlockOfSharesManager>().Use<BlockOfSharesManager>();
            this.For<IDataBaseInitializer>().Use<DataBaseInitializer>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<ITimeManager>().Use<TimeManager>();
            this.For<ILoggerService>().Use<LoggerService>();
            this.For<IBlockOfSharesRepository>().Use<BlockOfSharesRepository>();
            this.For<IClientRepository>().Use<ClientRepository>();
            this.For<IStockRepository>().Use<StockRepository>();
            this.For<ITransactionRepository>().Use<TransactionRepository>();
        }
    }
}