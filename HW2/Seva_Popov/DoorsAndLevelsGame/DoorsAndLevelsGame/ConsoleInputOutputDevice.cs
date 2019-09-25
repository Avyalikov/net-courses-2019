using DoorsAndLevelsGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame
{
    class ConsoleInputOutputDevice : IInputOutputDevice
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
            Console.WriteLine(dataToOutput);
        }

        public void WriteOutputArray(int[] doorNumbersArray)
        {
            foreach (var item in doorNumbersArray)
            {
                Console.Write(item + " ");
            }
        }
    }
}
