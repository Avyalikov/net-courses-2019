using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.TradesEmulator.Dependecies;


namespace Trading.TradesEmulator
{
    class Program
    {
        static void Main()
        {
            var container = new Container(new TradesEmulatorRegistry());
            var trading = container.GetInstance<ITradesEmulator>();

            trading.Run();
        }
    }
}
