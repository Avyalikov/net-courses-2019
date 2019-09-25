using System;
using System.Collections.Generic;
using System.Text;

namespace DoorsAndLevels
{
    class InputOutputModule : Interfaces.IInputOutputModule
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string outputData)
        {
            Console.WriteLine(outputData);
        }
    }
}
