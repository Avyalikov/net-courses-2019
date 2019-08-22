using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.Core.Services;
using System.Timers;

namespace Trading.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegestry());
            var tradeInteractiveService = container.GetInstance<TradingInteractiveService>();
            var tradeOperationService = container.GetInstance<TradingOperationService>();
            using (var dbContext = container.GetInstance<TradingDBContext>())
            {
                dbContext.Database.Initialize(false);
                Timer operationTimer = new Timer(10000) { AutoReset = true };
                operationTimer.Elapsed += (sender, e) => { tradeOperationService.ClientsTrade(); };
                operationTimer.Start();

                tradeInteractiveService.Run();
            }
        }

    }
}
