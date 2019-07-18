using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    class ConsoleIOProvider : IInputOutputProvider
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
