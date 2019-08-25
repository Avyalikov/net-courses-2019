namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System.Collections.Generic;
    using StructureMap;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.ConsoleClient.Repositories;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;
    using TradingSoftware.Services;

    public class CommandStrategyRegistry : Registry
    {
        public CommandStrategyRegistry()
        {
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

            this.For<ICommandStrategy>().Add<HelpCommandStrategy>();
            this.For<ICommandStrategy>().Add<InitiateDBCommand>();
            this.For<ICommandStrategy>().Add<ReadAllBlockOfSharesStrategy>();
            this.For<ICommandStrategy>().Add<ReadAllClientsStrategy>();
            this.For<ICommandStrategy>().Add<ReadAllSharesStrategy>();
            this.For<ICommandStrategy>().Add<ReadAllTransactions>();
            this.For<ICommandStrategy>().Add<StartSimulationRandomTransactionStrategy>();
            this.For<ICommandStrategy>().Add<StopSimulationRandomTransactionStrategy>();
            this.For<ICommandStrategy>().Add<BankruptRandomClientStrategy>();
            this.For<ICommandStrategy>().Add<ShowClientsWithOrangeStatusStrategy>();
            this.For<ICommandStrategy>().Add<ShowClientsWithBlackStatusStrategy>();
            this.For<ICommandStrategy>().Add<ReduceAssetsRandomClientStrategy>();
            this.For<ICommandStrategy>().Add<ManualAddClientStrategy>();
            this.For<ICommandStrategy>().Add<ManualAddShareStrategy>();
            this.For<ICommandStrategy>().Add<ManualAddTransactionStrategy>();
            this.For<ICommandStrategy>().Add<ManualAddBlockOfSharesStrategy>();

            this.For<IEnumerable<ICommandStrategy>>().Use(x => x.GetAllInstances<ICommandStrategy>());
        }
    }
}