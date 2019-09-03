using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithread.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new WikiConnectedLinksParser().Start();
        }
    }
}
