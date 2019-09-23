using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.Interfaces;
using TradeSimulator.Client.Misc;
using TradeSimulator.Client.Modules;

namespace TradeSimulator.Client.DependencyInjection
{
    public class TradeSimulatorClientRegistry : Registry
    {
        public TradeSimulatorClientRegistry()
        {
            this.For<IInputOutput>().Use<IOModule>();
            this.For<IPhraseProvider>().Use<JsonPhraseProviderModule>();
            this.For<ISettingsProvider>().Use<SettingsProviderModule>();
            this.For<RequestSenderModule>().Use<RequestSenderModule>();

            this.For<ClientApp>().Use<ClientApp>();
        }
    }
}
