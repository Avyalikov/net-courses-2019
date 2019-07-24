using System;

namespace doors_and_levels_game
{
    public class ConsoleIO : IInputOutputDevice
    {
        public string ReadInput()
        {
           return  Console.ReadLine;
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public void WriteOutput(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }
    }
}

