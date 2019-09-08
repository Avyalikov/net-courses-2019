using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;
using stockSimulator.WevServer.Repositories;
using StructureMap;
using System.Configuration;

namespace stockSimulator.WevServer.Dependecies
{
    public class StockSimulatorRegistry : Registry
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
            this.For<TransactionHistoryService>().Use<TransactionHistoryService>();

            this.For<StockSimulatorDbContext>().Use<StockSimulatorDbContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["stockSimulatorConnectionString"].ConnectionString);
        }
    }
}
