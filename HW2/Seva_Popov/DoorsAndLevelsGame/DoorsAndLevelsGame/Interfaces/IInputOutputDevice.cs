using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevelsGame.Interfaces
{
    interface IInputOutputDevice
    {
        string ReadInput();
        char ReadKey();
        void WriteOutput(string dataToOutput);
        void WriteOutputArray(int[] doorNumbersArray);
    }
}
