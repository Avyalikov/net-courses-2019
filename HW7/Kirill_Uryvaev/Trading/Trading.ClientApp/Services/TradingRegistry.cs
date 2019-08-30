using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Services;

namespace Trading.ClientApp
{
    class TradingRegistry: Registry
    {
        public TradingRegistry()
        {
            For<IValidator>().Use<TradeValidator>();
            For<ILogger>().Use<Log4NetLogger>().Ctor<bool>().Is(true).Named("OperationLogger");
            For<ILogger>().Use<Log4NetLogger>().Ctor<bool>().Is(false).Named("InteractionLogger");
            //For<TradeSimulator>().Use<TradeSimulator>().Ctor<ILogger>().Named("OperationLogger");
            For<RequestSender>().Use<RequestSender>();
        }
    }
}
