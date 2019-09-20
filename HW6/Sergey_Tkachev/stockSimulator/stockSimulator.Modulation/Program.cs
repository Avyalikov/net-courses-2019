using System;
using System.Timers;

namespace stockSimulator.Modulation
{
    class Program
    {
        static void Main(string[] args)
        {
            const int period = 10000;
            const bool dbInitialize = true;

            Simutator simulator = new Simutator(period, dbInitialize);

            simulator.start();

            simulator.stop();

        }
    }
}
