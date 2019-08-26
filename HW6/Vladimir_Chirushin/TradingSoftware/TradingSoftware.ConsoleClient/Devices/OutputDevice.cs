namespace TradingSoftware.ConsoleClient.Devices
{
    using System;

    public class OutputDevice : IOutputDevice
    {
        public void WriteLine(string outputString)
        {
            Console.WriteLine(outputString);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
