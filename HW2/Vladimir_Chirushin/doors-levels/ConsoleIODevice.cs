using System;

namespace doors_levels
{
    public class ConsoleIODevice : IInputOutputDevice
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string dataToOutput)
        {
            Console.WriteLine(dataToOutput);
        }
    }
}
