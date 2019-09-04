using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Simutator simulator = new Simutator();

            simulator.start();

            simulator.stop();
        }
    }
}
