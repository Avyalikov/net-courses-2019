using stockSimulator.Modulation.Dependencies;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Modulation
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using (var db = new StockSimulatorDbContext())
            {
                db.Database.Initialize(true);
                var container = new Container(new StockSimulatorRegistry());

                //Console.ReadKey();
            }

        }
    }
}
