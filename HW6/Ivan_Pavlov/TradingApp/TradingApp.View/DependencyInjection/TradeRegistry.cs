namespace TradingApp.View.DependencyInjection
{
    using StructureMap;
    using TradingApp.Core;
    using TradingApp.Core.ProxyForServices;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.View.Repositories;

    public class TradeRegistry : Registry
    {
        public TradeRegistry()
        {
            this.For<IUserTableRepository>().Use<UserTableRepository>();
            this.For<IPortfolioTableRepository>().Use<PortfolioTableRepository>();
            this.For<IShareTableRepository>().Use<ShareTableRepository>();
            this.For<ITransactionTableRepository>().Use<TransactionTableRepository>();
            this.For<TradingAppDb>().Use<TradingAppDb>();
            this.For<IUsersService>().Use<UsersProxy>();
            this.For<IShareServices>().Use<ShareProxy>();
            this.For<IPortfolioServices>().Use<PortfolioProxy>();
            this.For<ITransactionServices>().Use<TransactionProxy>();
            this.For<SimulatorOfTrading>().Use<SimulatorOfTrading>();
        }
    }
}
