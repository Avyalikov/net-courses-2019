namespace ReferenceCollector.Core.Services
{
    using System;
    public class ConsoleIoDevice : IIoDevice
    {
        public void Print(string data)
        {
            Console.WriteLine(data);
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }
    }
}
