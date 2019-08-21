namespace TradingSimulator
{
    using StructureMap;
    using DataBase;
    using Components;
    using log4net;
    using log4net.Config;

    class Program
    {
        static void Main()
        {

            using (var db = new TradingDbContext())
            {
                db.Database.Initialize(false);
            }
            
            var container = new Container(new TradingSimulatorRegistry());

            var game = container.GetInstance<IController>();
            game.Run();
        }
    }
}