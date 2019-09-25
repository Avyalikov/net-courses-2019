using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Repositories;
using TradingApp.Core.Services;
using TradingConsoleApp.Repositories;

namespace TradingConsoleApp.Dependencies
{
    public class TradingAppRegistry : Registry
    {
        public TradingAppRegistry()
        {
            this.For<IBalanceTableRepository>().Use<BalanceTableRepository>();
            this.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            this.For<IStockTableRepository>().Use<StockTableRepository>();
            this.For<IUserTableRepository>().Use<UserTableRepository>();
            this.For<BalancesService>().Use<BalancesService>();
            this.For<StocksService>().Use<StocksService>();
            this.For<UsersService>().Use<UsersService>();
            this.For<TransactionService>().Use<TransactionService>();
            this.For<ValidationOfTransactionService>().Use<ValidationOfTransactionService>();
            this.For<TradingAppDbContext>().Use<TradingAppDbContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingAppConnectionString"].ConnectionString);
        }

    }

}
