using StructureMap;
using TradingApiClient.DependencyInjection;

namespace TradingApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingApiClientRegistry());
            var tradingEngine = container.GetInstance<ITradingApiClientEngine>();

            tradingEngine.Run();
        }
    }
}
