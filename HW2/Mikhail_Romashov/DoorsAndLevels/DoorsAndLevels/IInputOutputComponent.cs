using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    public interface IInputOutputComponent
    {
        string ReadInputLine();
        char ReadInputKey();
        void WriteOutputLine(string dataToOutput);
        void WriteOutputLine();
        void WriteOutput(string dataToOutput);
    }
}
