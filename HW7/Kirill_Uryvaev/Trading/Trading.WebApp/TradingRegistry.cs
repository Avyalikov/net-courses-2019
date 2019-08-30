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
            For<IClientService>().Use<ClientService>();
            For<ClientsController>().Use<ClientsController>();
            For<IClientRepository>().Use<ClientRepository>();
            For<TradingDBContext>().Use<TradingDBContext>();
        }
    }
}
