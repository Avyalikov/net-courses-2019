namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using StructureMap;
    using System.Collections.Generic;

    class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            this.For<IInputOutputModule>().Use<InputOutputModule>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<IMainLoop>().Use<MainLoop>();
            this.For<IHTTPRequestService>().Use<HTTPRequestService>();
            this.For<ICommand>().Add<HelpCommand>();
            this.For<ICommand>().Add<ManualAddClientCommand>();
            this.For<ICommand>().Add<ManualAddPortfolioCommand>(); 
            this.For<ICommand>().Add<RemoveClientCommand>();
            this.For<ICommand>().Add<RemovePortfolioCommand>();
            this.For<ICommand>().Add<GetBalanceCommand>();
            this.For<ICommand>().Add<GetClientsCommand>();
            this.For<ICommand>().Add<GetPortfoliosCommand>();
            this.For<ICommand>().Add<GetTransactionsCommand>();
            this.For<ICommand>().Add<MakeDealCommand>();
            this.For<ICommand>().Add<UpdateClientCommand>();
            this.For<ICommand>().Add<UpdatePortfolioCommand>();
            this.For<IEnumerable<ICommand>>().Use(x => x.GetAllInstances<ICommand>());
        }
    }
}
