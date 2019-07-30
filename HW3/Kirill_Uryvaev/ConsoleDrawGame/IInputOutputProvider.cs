using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDrawGame
{
    interface IInputOutputProvider
    {
        void Write(char symbol);
        void WriteLine(string text);
        string Read();
    }
}
