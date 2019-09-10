using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class ConsoleOutputProvider : IOutputProvider
    {
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
    }
}
