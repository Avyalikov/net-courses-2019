namespace Multithread.ConsoleApp
{
    using Multithread.ConsoleApp.DependencyInjection;
    using Multithread.Core.Services;
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
            var container = new Container(new ConnectedLinksRegistry());

            new ConnectedLinksParser(container.GetInstance<ParsingService>())
                .Start();
        }
    }
}
