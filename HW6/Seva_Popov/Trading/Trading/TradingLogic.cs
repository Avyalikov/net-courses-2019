using System;
using System.Collections.Generic;
using System.Text;
using Trading.Interfaces;

namespace Trading
{
    class TradingLogic : ITradingLogic
    {
        public TradingLogic(
            )
        {

        }
        public void Run()
        {
            Console.WriteLine("Start");
        }
    }
}
