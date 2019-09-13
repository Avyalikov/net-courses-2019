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

            multithread.Run().Wait();
            Console.Write("Application completed");
            Console.ReadKey();
        }
    }
}