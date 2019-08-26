using StructureMap;
using System;
using System.Linq;
using TradingSimulator.ConsoleApp.Dependency;
using TradingSimulator.ConsoleApp.Repositories;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingSimulatorRegistry());
            var tradingStart = container.GetInstance<ITradingStart>();
            tradingStart.Run();
        }
    }
}
