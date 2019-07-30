using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    class ConsoleIOProvider: IInputOutputProvider
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(char symbol)
        {
            Console.Write(symbol);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
