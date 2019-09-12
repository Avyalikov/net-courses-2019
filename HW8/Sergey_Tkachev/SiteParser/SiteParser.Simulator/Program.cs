using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string startPageToParse = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";

            Simulator simulator = new Simulator(startPageToParse);

            simulator.Start();

            Console.ReadLine();
        }
    }
}
