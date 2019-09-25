namespace TradingApp.Services
{
    using TradingApp.Interfaces;
    using System;

    class InputOutputModule : IInputOutputModule
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string outputData)
        {
            Console.WriteLine(outputData);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
