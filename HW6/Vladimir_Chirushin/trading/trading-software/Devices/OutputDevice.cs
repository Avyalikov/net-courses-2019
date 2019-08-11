using System;

namespace trading_software
{
    public class OutputDevice : IOutputDevice
    {
        public void WriteLine(string OutputString)
        {
            Console.WriteLine(OutputString);
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}
