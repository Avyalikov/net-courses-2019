using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApiClient.DependencyInjection
{
    using StructureMap;
    using TradingApiClient.Devices;
    using TradingApiClient.Services;

    public class TradingApiClientRegistry : Registry
    {
        public TradingApiClientRegistry()
        {
            this.For<ITradingApiClientEngine>().Use<TradingApiClientEngine>();

            this.For<IOutputDevice>().Use<OutputDevice>();
            this.For<IInputDevice>().Use<InputDevice>();
            this.For<IHttpRequestManager>().Use<HttpRequestManager>();
            this.For<ICommandParser>().Use<CommandParser>();
        }
    }
}
