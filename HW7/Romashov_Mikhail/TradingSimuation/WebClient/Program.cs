using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebClient.Components;

namespace WebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TradingData tradingData = new TradingData();
            TradingSimulation tradingSimulation = new TradingSimulation();
            Timer operationTimer = new Timer(10000) { AutoReset = true };
            operationTimer.Elapsed += (sender, e) => { tradingSimulation.TradingSimulate(); };
            operationTimer.Enabled = true;

            tradingData.Run();
        }
    }
}
