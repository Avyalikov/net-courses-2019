namespace Traiding.ConsoleApp
{
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
            new StockExchange(
                new Container(new DependencyInjection.TraidingRegistry())
                ).Start();
        }
    }
}
