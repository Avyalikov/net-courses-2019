using System;
using System.Collections.Generic;
using System.Text;
using Trading.Interfaces;

namespace Trading.Components
{
    class OutputData : IOutputData
    {
        public void WriteOutput(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }
    }
}
