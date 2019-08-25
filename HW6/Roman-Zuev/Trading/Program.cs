using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using StructureMap;

namespace Trading.DataModel
{
    class Program
    {
        static void Main()
        {
            var container = new Container(new TradingRegistry());
            var trading = container.GetInstance<ITradingCore>();

            trading.Run();

            //using (TradingDBContext db = new TradingDBContext())
            //{
            //    Client seller = db.Clients.Where(c => c.ClientID == 2).First();
            //    Console.WriteLine(seller.Name);
            //}
        }
    }
}
