namespace TradingApp
{
    using Interfaces;
    using DependencyInjection;
    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegistry());
            var mainLoop = container.GetInstance<IMainLoop>();
            mainLoop.Start();
        }
    }
}
