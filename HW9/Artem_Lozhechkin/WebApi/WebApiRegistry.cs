namespace TradingApp.WebApi
{
    using StructureMap;
    using System.Configuration;
    using TradingApp.Core.LoggingServices;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;
    using TradingApp.Shared;
    using TradingApp.Shared.Repositories;

    public class WebApiRegistry : Registry
    {
        public WebApiRegistry()
        {
            this.For<IRepository<TraderEntity>>().Use<TraderTableRepository>();
            this.For<IRepository<CompanyEntity>>().Use<CompanyTableRepository>();
            this.For<IRepository<ShareEntity>>().Use<ShareTableRepository>();
            this.For<IRepository<TransactionEntity>>().Use<TransactionTableRepository>();
            this.For<IRepository<StockEntity>>().Use<StockTableRepository>();
            this.For<IRepository<ShareTypeEntity>>().Use<ShareTypeTableRepository>();

            this.For<TradingAppDbContext>().Use<TradingAppDbContext>();

            this.For<ShareService>().Use<LoggingShareService>();
            this.For<TraderService>().Use<LoggingTraderService>();
            this.For<TransactionService>().Use<LoggingTransactionService>();

            this.For<TradingAppDbContext>().Use<TradingAppDbContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingAppConnectionString"].ConnectionString);
        }
    }
}
