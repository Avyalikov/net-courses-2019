using StructureMap;
using System.Configuration;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.TradesEmulator.Models;
using Trading.TradesEmulator.Repositories;
using Trading.TradesEmulator.Services;

namespace Trading.TradesEmulator.Dependecies
{
    public class TradesEmulatorRegistry: Registry
    {
        public TradesEmulatorRegistry()
        {
            this.For<IClientTableRepository>().Use<ClientTableRepository>();
            this.For<IClientSharesTableRepository>().Use<ClientSharesTableRepository>();
            this.For<ISharesTableRepository>().Use<SharesTableRepository>();
            this.For<ITransactionHistoryTableRepository>().Use<TransactionHistoryTableRepository>();
            this.For<IClientsService>().Use<ClientsService>();
            this.For<ITransactionService>().Use<TransactionServiceProxy>();
            this.For<ITradesEmulator>().Use<TradesEmulator>();
            this.For<LoggerLog4Net>().Use<LoggerLog4Net>();
            this.For<TradesEmulatorDbContext>().Use<TradesEmulatorDbContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["TradesEmulatorConnectionString"].ConnectionString);
        }
    }
}
