using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSimulator.Client.Interfaces
{
    internal interface IInputOutput
    {
        string ReadInput();

        char ReadKey();

        void WriteOutput(string dataToOutput);

        void ClearMenu();
    }
}
