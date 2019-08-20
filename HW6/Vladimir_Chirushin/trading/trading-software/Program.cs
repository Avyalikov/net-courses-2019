namespace trading_software
{
    using StructureMap;
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