namespace TradingApiClient
{
    using StructureMap;
    using TradingApiClient.DependencyInjection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container(new TradingApiClientRegistry());
            var tradingEngine = container.GetInstance<ITradingApiClientEngine>();

            tradingEngine.Run();
        }
    }
}
