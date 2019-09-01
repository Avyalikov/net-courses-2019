using StructureMap;
using System.Timers;
using TradingSimulator.ConsoleApp.Components;
using TradingSimulator.ConsoleApp.Dependencies;

namespace TradingSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingDataRegistry());

            var tradeData = container.GetInstance<TradingData>();

            var tradeSimulation = container.GetInstance<TradeSimulation>();

            using (var dbContext = container.GetInstance<TradingSimulatorDBContext>())
            {
                dbContext.Database.Initialize(false);
                Timer operationTimer = new Timer(10000) { AutoReset = true };
                operationTimer.Elapsed += (sender, e) => { tradeSimulation.Run(); };
                operationTimer.Enabled = true;

                tradeData.Run();
            }
        }
    }
}
