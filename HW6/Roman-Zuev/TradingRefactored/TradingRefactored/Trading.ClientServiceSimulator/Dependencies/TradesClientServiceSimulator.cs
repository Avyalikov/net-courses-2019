using StructureMap;
using System.Configuration;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.TradesEmulator;
using Trading.TradesEmulator.Models;
using Trading.TradesEmulator.Repositories;
using Trading.TradesEmulator.Services;

namespace Trading.ClientServiceSimulator.Dependecies
{
    public class TradesClientServiceSimulator: Registry
    {
        public TradesClientServiceSimulator()
        {
            this.For<IClientTableRepository>().Use<ClientTableRepository>();
            this.For<IClientSharesTableRepository>().Use<ClientSharesTableRepository>();
            this.For<ISharesTableRepository>().Use<SharesTableRepository>();
            this.For<IClientsService>().Use<ClientsService>();
            this.For<TradesEmulatorDbContext>().Use<TradesEmulatorDbContext>().Ctor<string>("connectionString")
                .Is(ConfigurationManager.ConnectionStrings["TradesEmulatorConnectionString"].ConnectionString);
        }
    }
}
