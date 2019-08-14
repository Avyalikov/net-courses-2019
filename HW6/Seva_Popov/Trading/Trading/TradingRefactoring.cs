using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Interfaces;

namespace Trading
{
    class TradingRefactoring : Registry
    {
        public TradingRefactoring()
        {
            this.For<ITradingLogic>().Use<TradingLogic>();
        }
    }
}
