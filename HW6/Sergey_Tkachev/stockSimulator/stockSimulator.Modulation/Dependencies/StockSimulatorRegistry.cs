namespace stockSimulator.Modulation.Dependencies
{
    using System.Configuration;
    using stockSimulator.Core.Repositories;
    using stockSimulator.Core.Services;
    using stockSimulator.Modulation.Repositories;
    using StructureMap;

    internal class StockSimulatorRegistry : Registry
    {
        /// <summary>
        /// Initializes an Instance of StockSimulatorRegistry class.
        /// </summary>
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
