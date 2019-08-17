using StructureMap;

namespace trading_software
{
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
            this.For<IDataBaseDevice>().Use<DataBaseDevice>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<ITimeManager>().Use<TimeManager>();
        }
    }
}