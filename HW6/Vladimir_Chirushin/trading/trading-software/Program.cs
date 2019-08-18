using trading_software.Services;
using StructureMap;

namespace trading_software
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingSoftwareRegistry());
            var tradingEngine = container.GetInstance<ITradingEngine>();

            tradingEngine.Run();
        }
    }
}
