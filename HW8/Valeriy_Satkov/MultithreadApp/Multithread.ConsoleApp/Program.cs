namespace Multithread.ConsoleApp
{
    using Multithread.ConsoleApp.DependencyInjection;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            new ConnectedLinksParser(new Container(new ConnectedLinksRegistry())).Start();
        }
    }
}
