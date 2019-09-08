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
            RequestsSimutator simulator = new RequestsSimutator();

            simulator.start();

           // simulator.stop();
        }
    }
}
