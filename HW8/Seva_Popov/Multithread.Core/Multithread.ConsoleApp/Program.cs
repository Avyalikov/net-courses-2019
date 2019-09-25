using System;
using StructureMap;
using System.ComponentModel;
using Multithread.ConsoleApp.Dependency;

namespace Multithread.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new StructureMap.Container(new MultithreadRegistry());
            var startApp = container.GetInstance<StartApp>();
            startApp.Run();
        }
    }
}
