using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class ConsoleInputProvider : IInputProvider
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
