using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Client.Interfaces;

namespace TradeSimulator.Client.Modules
{
    internal class IOModule : IInputOutput
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

        public void ClearMenu()
        {
            Console.Clear();
        }
    }
}
