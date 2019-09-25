namespace TradingApp.View.Provider
{
    using System;
    using TradingApp.View.Interface;

    class ConsoleIO : IIOProvider
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
