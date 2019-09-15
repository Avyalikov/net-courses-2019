using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.Core;
using Trading.Core.Services;
using Trading.WebApp.Controllers;
using Trading.Core.Repositories;
using Trading.WebApp.Repositories;

namespace Trading.WebApp
{
    class TradingRegistry : Registry
    {
        public TradingRegistry()
        {
            For<ClientsController>().Use<ClientsController>();
            For<SharesController>().Use<SharesController>();
            For<BalancesController>().Use<BalancesController>();
            For<DealController>().Use<DealController>();
            For<TransactionsController>().Use<TransactionsController>();

            For<IClientRepository>().Use<ClientRepository>();
            For<IClientsSharesRepository>().Use<ClientSharesRepository>();
            For<ITransactionHistoryRepository>().Use<TransactionRepository>();
            For<IBalanceRepository>().Use<BalanceRepository>();

            For<TradingDBContext>().Use<TradingDBContext>();
            For<IValidator>().Use<TradeValidator>();
            For<BalanceService>().Use<BalanceService>();
            For<IClientService>().Use<ClientService>();
            For<IClientsSharesService>().Use<ClientsSharesService>();
            For<TransactionHistoryService>().Use<TransactionHistoryService>();
        }
    }
}
