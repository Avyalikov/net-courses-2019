using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class ConsoleInput : IReadInputProvider
    {
        public string readInput()
        {
            return Console.ReadLine();
        }
    }
}