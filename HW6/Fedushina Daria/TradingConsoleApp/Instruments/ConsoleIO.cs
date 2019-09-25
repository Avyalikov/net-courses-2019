using System;
using TradingConsoleApp.Interfaces;

namespace TradingConsoleApp.Instruments
{
     class ConsoleIO : IDeviceIO
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