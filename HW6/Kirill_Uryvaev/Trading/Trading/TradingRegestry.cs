using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Trading
{
    class TradingRegestry: Registry
    {
        public TradingRegestry()
        {
            For<IPhraseProvider>().Use<JsonPhraseProvider>();
            For<IIOProvider>().Use<ConsoleIOProvider>();
            For<IValidator>().Use<TradeValidator>();
            For<IOperations>().Use<ClientsOperations>();
            For<ITrade>().Use<TradeManager>();
        }
    }
}
