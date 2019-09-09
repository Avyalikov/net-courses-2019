namespace TradingAppWebAPI
{
    using TradingApp.Core.Interfaces;
    using TradingApp.Core.Repos;
    using TradingApp.Core.Services;
    using Controllers;
    using Repos;
    using StructureMap;

    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            For<ClientsController>().Use<ClientsController>();
            For<PortfoliosController>().Use<PortfoliosController>();
            For<BalanceController>().Use<BalanceController>();
            For<DealController>().Use<DealController>();
            For<TransactionsController>().Use<TransactionsController>();

            For<IClientRepository>().Use<ClientRepository>();
            For<IPortfolioRepository>().Use<PortfolioRepository>();
            For<ITransactionsRepository>().Use<TransactionsRepository>();

            For<DBContext>().Use<DBContext>();
            For<IValidationService>().Use<ValidationService>();
            For<IClientService>().Use<ClientService>();
            For<IPortfoliosService>().Use<PortfoliosService>();
            For<ITransactionsService>().Use<TransactionsService>();
            For<ILogger>().Use<Logger>();
        }
    }
}