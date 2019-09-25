using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core;
using TradeSimulator.Core.Repositories;
using TradeSimulator.Core.Services;
using TradeSimulator.Server.Controllers;
using TradeSimulator.Server.DbInit;
using TradeSimulator.Server.Repositories;

namespace TradeSimulator.Server
{
    public class TradeSimulatorServerRegistry : Registry
    {
        public TradeSimulatorServerRegistry()
        {
            this.For<IAccountTableRepository>().Use<AccountTableRepository>();
            this.For<IClientsTableRepository>().Use<ClientsTableRepository>();
            this.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            this.For<IStockOfClientTableRepository>().Use<StockOfClientTableRepository>();
            this.For<IStockPriceTableRepository>().Use<StockPriceTableRepository>();

            this.For<ILogger>().Use<Logger>();

            this.For<ClientsService>().Use<ClientsService>();
            this.For<ShowDbInfoService>().Use<ShowDbInfoService>();
            this.For<TradingService>().Use<TradingService>();

            this.For<ClientsController>().Use<ClientsController>();
            this.For<BalanceController>().Use<BalanceController>();
            this.For<SharesController>().Use<SharesController>();
            this.For<TradingController>().Use<TradingController>();
            this.For<TransactionHistoryController>().Use<TransactionHistoryController>();

            this.For<TradeSimDbContext>().Use<TradeSimDbContext>();
        }
    }
}
