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

        public void WriteLineOutput(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }

        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }
    }
}
