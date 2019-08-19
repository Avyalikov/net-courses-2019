namespace TradingSimulator
{
    using StructureMap;
    using DataBase;

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