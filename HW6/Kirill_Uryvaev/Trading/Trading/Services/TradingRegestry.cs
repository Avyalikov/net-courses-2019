using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.Core.Services;
using Trading.Core;
using Trading.Core.Repositories;
using Trading.ConsoleApp.Repositories;

namespace Trading.ConsoleApp
{
    class TradingRegestry: Registry
    {
        public TradingRegestry()
        {
            For<IPhraseProvider>().Use<JsonPhraseProvider>();
            For<IIOProvider>().Use<ConsoleIOProvider>();
            For<IValidator>().Use<TradeValidator>();
            For<IClientService>().Use<ClientService>();
            For<IShareService>().Use<ShareService>();
            For<IClientsSharesService>().Use<ClientsSharesService>();
            For<TradeSimulator>().Use<TradeSimulator>().Ctor<ILogger>().Named("OperationLogger");
            For<TradingInteractiveService>().Use<TradingInteractiveService>().Ctor<ILogger>().Named("InteractionLogger");
            For<IClientRepository>().Use<ClientRepository>();
            For<IClientsSharesRepository>().Use<ClientsSharesRepository>();
            For<IShareRepository>().Use<ShareRepository>();
            For<TradingDBContext>().Use<TradingDBContext>().AlwaysUnique();
            For<ILogger>().Use<Log4netLogger>().Ctor<bool>().Is(true).Named("OperationLogger");
            For<ILogger>().Use<Log4netLogger>().Ctor<bool>().Is(false).Named("InteractionLogger");
        }
    }
}
