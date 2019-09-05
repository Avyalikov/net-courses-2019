using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Dependencies;
using StructureMap;
using System;

namespace MultithreadConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new MultithreadRegistry());

            var multithread = container.GetInstance<Multithread>();
            using (var dbContext = container.GetInstance<LinksDBContext>())
            {
                dbContext.Database.Initialize(false);

                multithread.Run();




                Console.ReadKey();
            }
            
        }

      
    }
}
