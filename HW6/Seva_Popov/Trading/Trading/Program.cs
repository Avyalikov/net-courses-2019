using StructureMap;
using System;
using Trading.Interfaces;

namespace Trading
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegistry());
            var tradingLogic = container.GetInstance<ITradingLogic>();
            tradingLogic.Run();
        }
    }
}
