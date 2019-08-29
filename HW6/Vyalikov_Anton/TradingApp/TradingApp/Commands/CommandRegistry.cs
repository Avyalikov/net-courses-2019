namespace TradingApp.Commands
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Repos;
    using TradingApp.Core.Services;
    using Interfaces;
    using Repos;
    using Services;
    using StructureMap;
    using System.Collections.Generic;

    class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            this.For<IInputOutputModule>().Use<InputOutputModule>();
            this.For<IClientService>().Use<ClientService>();
            this.For<ISharesService>().Use<SharesService>();
            this.For<IPortfoliosService>().Use<PortfoliosService>();
            this.For<ITransactionsService>().Use<TransactionsService>();
            this.For<IClientRepository>().Use<ClientRepository>();
            this.For<ISharesRepository>().Use<SharesRepository>();
            this.For<IPortfolioRepository>().Use<PortfolioRepository>();
            this.For<ITransactionsRepository>().Use<TransactionsRepository>();
            this.For<IConsoleApp>().Use<ConsoleApp>();
            this.For<IDBInitializer>().Use<DBInitializer>();
            this.For<ITradeTable>().Use<TradeTable>();
            this.For<ITradingSimulation>().Use<TradingSimulation>();
            this.For<ITradingTimer>().Use<TradingTimer>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<ILogger>().Use<Logger>();
            this.For<IValidationService>().Use<ValidationService>();
            this.For<ICommand>().Add<HelpCommand>();
            this.For<ICommand>().Add<DBInitiateCommand>();
            this.For<ICommand>().Add<GetAllClientsCommand>();
            this.For<ICommand>().Add<GetAllSharesCommand>();
            this.For<ICommand>().Add<GetAllPortfoliosCommand>();
            this.For<ICommand>().Add<GetAllTransactionsCommand>();
            this.For<ICommand>().Add<GetBlackZoneCommand>();
            this.For<ICommand>().Add<GetOrangeZoneCommand>();
            this.For<ICommand>().Add<ManualAddClientCommand>();
            this.For<ICommand>().Add<ManualAddShareCommand>();
            this.For<ICommand>().Add<ManualAddPortfolioCommand>();
            this.For<ICommand>().Add<StartSimulationCommand>();
            this.For<ICommand>().Add<StopSimulationCommand>();
            this.For<IEnumerable<ICommand>>().Use(x => x.GetAllInstances<ICommand>());

        }
    }
}
