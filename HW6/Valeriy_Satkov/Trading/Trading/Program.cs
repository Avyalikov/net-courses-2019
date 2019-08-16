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

            using (var context = new StockExchangeContext())
            {
                ioProvider.WriteLine(context.LoadingText());
                context.Database.Initialize(false);
                ioProvider.WriteLine(context.LoadingDoneText());

                new StockExchange(ioProvider: ioProvider, context: context).Start();
            }            
        }
    }
}
