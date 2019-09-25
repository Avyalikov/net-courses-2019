using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doors_and_levels_game_after_refactoring
{
    public class ConsoleInputOutputDevice : IInputOutputDevice
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
    }
}
