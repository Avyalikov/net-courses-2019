namespace TradingSoftware.ConsoleClient
{
    using StructureMap;
    using TradingSoftware.ConsoleClient.DependencyInjection;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container(new TradingSoftwareRegistry());
            var tradingEngine = container.GetInstance<ITradingEngine>();

            tradingEngine.Run();
        }
    }
}
