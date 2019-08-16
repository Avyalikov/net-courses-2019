namespace Trading
{
    using System;
    using Interfaces;
    using Components;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputDevice ioProvider = new ConsoleInputOutputDevice();

            new StockExchange(ioProvider: ioProvider).Start();
        }
    }
}
