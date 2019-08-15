namespace Trading
{
    using System;
    using Trading.Infrastructure;
    using Trading.Logic;

    class Program
    {
        static void Main(string[] args)
        {
            TradeLogic tradeLogic = TradeLogic.Initialize();
            while (true)
            {
                tradeLogic.Run();
            }
        }
    }
}
