using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;
using stockSimulator.Modulation.Repositories;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Modulation.Dependencies
{
    class StockSimulatorRegistry : Registry
    {
        public StockSimulatorRegistry()
        {
            this.For<IClientTableRepository>().Use<ClientTableRepository>();
            this.For<IStockOfClientsTableRepository>().Use<StockOfClientsTableRepository>();
            this.For<IStockTableRepository>().Use<StockTableRepository>();
            this.For<ITransactionHistoryTableRepository>().Use<TransactionHistoryTableRepository>();

            this.For<ClientService>().Use<ClientService>();
            this.For<EditCleintStockService>().Use<EditCleintStockService>();
            this.For<StockService>().Use<StockService>();
            this.For<TransactionService>().Use<TransactionService>();

            this.For<StockSimulatorDbContext>().Use<StockSimulatorDbContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["stockSimulatorConnectionString"].ConnectionString);
        }
    }
}
