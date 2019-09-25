using System;
using TradingSimulatorConsoleApp.Data;

namespace TradingSimulatorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TradingSimulatorDbContext tradingSimulatorDbContext = new TradingSimulatorDbContext();
            TradingSimulation tradingSimulation = new TradingSimulation(tradingSimulatorDbContext);
            tradingSimulation.RunTradingSimulation();
        }
    }
}
