namespace WebApiTradingServer.Services.CommandStrategy
{
    using StructureMap;
    using System.Collections.Generic;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;
    using WebApiTradingServer.Repositories;

    public class CommandStrategyRegistry : Registry
    {
        public CommandStrategyRegistry()
        {
            this.For<IClientManager>().Use<ClientManager>();
            this.For<IShareManager>().Use<ShareManager>();
            this.For<ITransactionManager>().Use<TransactionManager>();
            this.For<IBlockOfSharesManager>().Use<BlockOfSharesManager>();
            this.For<ILoggerService>().Use<LoggerService>();
            this.For<IClientRepository>().Use<ClientRepository>();
            this.For<ISharesRepository>().Use<SharesRepository>();
            this.For<ITransactionRepository>().Use<TransactionRepository>();
            this.For<IBlockOfSharesRepository>().Use<BlockOfSharesRepository>();

        }
    }
}
