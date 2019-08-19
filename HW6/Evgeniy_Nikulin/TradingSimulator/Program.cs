namespace TradingSimulator
{
    using System;
    using System.Linq;
    using StructureMap;
    using Core;
    using Model;

    class Program
    {
        static void Main()
        {
            using (var db = new TradingDbContext())
            {
                db.Database.Initialize(false);
            }

            var container = new Container(new TradingSimulatorRegistry());
            var game = container.GetInstance<IGame>();

            game.Run();
            /*
            using (var db = new TradingDbContext())
            {
                var queary = db.Brokers.Select(b => new { b.Card, b.ShareList });

                foreach (var q in queary)
                {
                    Console.WriteLine($"Broker - {q.Card.Name} {q.Card.Surname}; Shares:");
                    foreach (var i in q.ShareList)
                    {
                        Console.WriteLine($"\t{i.Quantity} {i.Name}");
                    }

                    Console.WriteLine();
                }

                Console.ReadLine();
            }
            */
        }
    }
}