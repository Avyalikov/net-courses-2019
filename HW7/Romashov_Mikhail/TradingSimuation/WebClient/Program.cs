using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebClient.Components;

namespace WebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TradingData tradingData = new TradingData();
            tradingData.Run();
        }
    }
}
