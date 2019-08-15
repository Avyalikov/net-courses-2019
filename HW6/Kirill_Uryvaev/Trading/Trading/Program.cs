using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Trading
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegestry());
            var tradeManager = container.GetInstance<ITrade>();

            tradeManager.Run();
        }
    }
}
