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
        }
    }
}
