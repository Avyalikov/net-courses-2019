using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels.Interfaces
{
    interface IInputOutputModule
    {
        string ReadInput();

        void WriteOutput(string outputData);
    }
}
