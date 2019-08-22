using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;
using TradingSimulator.Dependencies;

namespace TradingSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingSimulatorRegistry());

            var simulate = container.GetInstance<TradeSimulation>();

            simulate.Run();
        }
    }
}
