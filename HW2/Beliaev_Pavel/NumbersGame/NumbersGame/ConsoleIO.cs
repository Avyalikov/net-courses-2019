using System;
using NumbersGame.Interfaces;

namespace NumbersGame
{
    class ConsoleIO : IInputOutput
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }
    }
}
