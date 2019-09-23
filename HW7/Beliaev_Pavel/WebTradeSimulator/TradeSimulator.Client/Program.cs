using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.DependencyInjection;

namespace TradeSimulator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradeSimulatorClientRegistry());

            ClientApp clientApp = container.GetInstance<ClientApp>();

            clientApp.Run();
        }
    }
}
