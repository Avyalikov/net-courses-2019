using StructureMap;
using System;
using Trading.Interfaces;

namespace Trading
{
    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container(new TradingRefactoring());
            var tradingLogic = container.GetInstance<ITradingLogic>();
            tradingLogic.Run();
        }
    }
}
