namespace TradingSoftware.ConsoleClient.DependencyInjection
{
    using StructureMap;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.ConsoleClient.Repositories;
    using TradingSoftware.ConsoleClient.Services;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;
    using TradingSoftware.Services;

    public class TradingSoftwareRegistry : Registry
    {
        public TradingSoftwareRegistry()
        {
            this.For<ITradingEngine>().Use<TradingEngine>();

            this.For<IOutputDevice>().Use<OutputDevice>();
            this.For<IInputDevice>().Use<InputDevice>();
            this.For<IClientManager>().Use<ClientManager>();
            this.For<IShareManager>().Use<ShareManager>();
            this.For<ITableDrawer>().Use<TableDrawer>();
            this.For<ITransactionManager>().Use<TransactionManager>();
            this.For<IBlockOfSharesManager>().Use<BlockOfSharesManager>();
            this.For<IDataBaseInitializer>().Use<DataBaseInitializer>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<ITimeManager>().Use<TimeManager>();
            this.For<ILoggerService>().Use<LoggerService>();
            this.For<ISimulationManager>().Use<SimulationManager>();
            this.For<IClientRepository>().Use<ClientRepository>();
            this.For<ISharesRepository>().Use<SharesRepository>();
            this.For<ITransactionRepository>().Use<TransactionRepository>();
            this.For<IBlockOfSharesRepository>().Use<BlockOfSharesRepository>();
            this.For<IUserInteractionManager>().Use<UserInteractionManager>();
        }
    }
}