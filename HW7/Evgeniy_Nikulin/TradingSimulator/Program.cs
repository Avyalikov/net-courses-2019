namespace TradingSimulator
{
    using StructureMap;

    class Program
    {
        static void Main()
        {
            var container = new Container(new TradingSimulatorRegistry());

            var trading = container.GetInstance<IController>();
            trading.Run();
        }
    }
}