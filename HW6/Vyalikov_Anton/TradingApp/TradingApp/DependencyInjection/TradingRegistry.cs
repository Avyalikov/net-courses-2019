namespace TradingApp.DependencyInjection
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Repos;
    using TradingApp.Core.Services;
    using TradingApp.Interfaces;
    using TradingApp.Repos;
    using TradingApp.Services;
    using StructureMap;

    class TradingRegistry : Registry
    {
        public TradingRegistry()
        {
            this.For<IDBInitializer>().Use<DBInitializer>();
            this.For<IInputOutputModule>().Use<InputOutputModule>();
            this.For<ITradeTable>().Use<TradeTable>();
            this.For<ITradingSimulation>().Use<TradingSimulation>();
            this.For<IClientService>().Use<ClientService>();
            this.For<ILogger>().Use<Logger>();
            this.For<IPortfoliosService>().Use<PortfoliosService>();
            this.For<ISharesService>().Use<SharesService>();
            this.For<ITransactionsService>().Use<TransactionsService>();
            this.For<IValidationService>().Use<ValidationService>();
            this.For<IClientRepository>().Use<ClientRepository>();
            this.For<IDBComm>().Use<DBComm>();
            this.For<IPortfolioRepository>().Use<PortfolioRepository>();
            this.For<ISharesRepository>().Use<SharesRepository>();
            this.For<ITransactionsRepository>().Use<TransactionsRepository>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<ITradingTimer>().Use<TradingTimer>();
            this.For<IConsoleApp>().Use<ConsoleApp>();
            this.For<IMainLoop>().Use<MainLoop>();
        }
    }
}
