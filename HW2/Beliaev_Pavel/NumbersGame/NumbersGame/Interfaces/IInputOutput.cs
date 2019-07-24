using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersGame
{
    public interface IInputOutput
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
    }
}
