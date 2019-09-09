namespace TradingApp.ConsoleTradingManager
{
    using StructureMap;
    using System.Data.Entity;
    using TradingApp.Core;
    using TradingApp.Shared;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingManagerRegistry());
            Database.SetInitializer(new SeedInitializer());
            Logger.InitLogger();

            ConsoleManager cm = container.GetInstance<ConsoleManager>();

            cm.Start();
        }
    }
}
