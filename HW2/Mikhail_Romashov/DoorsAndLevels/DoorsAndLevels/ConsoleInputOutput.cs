using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    public class ConsoleInputOutput : IInputOutputComponent
    {
        public string ReadInputLine()
        { 
            return Console.ReadLine();
        }
        public char ReadInputKey()
        {
            return Console.ReadKey().KeyChar;
        }
        public void WriteOutputLine(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }
        public void WriteOutputLine()
        {
            Console.WriteLine();
        }
        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }
    }
}
