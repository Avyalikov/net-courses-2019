using StructureMap;
using ReferenceCollectorApp.Dependencies;
using ReferenceCollectorApp.Services;
using ReferenceCollectorApp.Repositories;
using System;

namespace ReferenceCollectorApp
{
    class Program
    {
        static void Main()
        {
            var container = new Container(new ReferenceCollectorRegistry());
            var program = container.GetInstance<IReferenceCollector>();
            program.Run();
            //var table = new ReferencesTable(new Context.ReferenceCollectorDbContext());
            //var service = new ReferenceCollectorService(table);
            //var dic = service.AddRefsToDictionary(service.DownloadPage("/wiki/Victory_Day_(Malta)", @".\resources\").Result, 1);
            //foreach (var item in dic)
            //{
            //    Console.WriteLine(item.Key);
            //}
            //Console.ReadLine();
        }
    }
}
