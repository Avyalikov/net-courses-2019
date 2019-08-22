using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.Core.Services;
using Trading.Core;

namespace Trading
{
    class TradingRegestry: Registry
    {
        public TradingRegestry()
        {
            For<IPhraseProvider>().Use<JsonPhraseProvider>();
            For<IIOProvider>().Use<ConsoleIOProvider>();
            For<IValidator>().Use<TradeValidator>();
            For<TradingOperationService>().Use<TradingOperationService>();
            For<TradingInteractiveService>().Use<TradingInteractiveService>();
        }
    }
}
