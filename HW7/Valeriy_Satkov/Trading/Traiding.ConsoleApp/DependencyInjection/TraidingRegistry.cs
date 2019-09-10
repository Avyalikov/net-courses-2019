namespace Traiding.ConsoleApp.DependencyInjection
{
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.Logger;

    public class TraidingRegistry : Registry
    {
        public TraidingRegistry()
        {
            For<RequestSender>().Use<RequestSender>();
            For<ILoggerService>().Use<Log4NetService>().Ctor<bool>().Is(true).Named("TradingSimLogger");
            For<ILoggerService>().Use<Log4NetService>().Ctor<bool>().Is(false).Named("InteractionLogger");
            For<TraidingSimulator>().Use<TraidingSimulator>().Ctor<ILoggerService>().Named("TradingSimLogger");
        }
    }
}
