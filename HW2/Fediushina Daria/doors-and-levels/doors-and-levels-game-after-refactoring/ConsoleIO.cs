using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    class ConsoleIO : IDeviceInOut
    {
        public string ReadOutput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string OutputData)
        {
            Console.WriteLine(OutputData);
        }
    }
}
