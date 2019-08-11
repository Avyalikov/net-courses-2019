namespace Trading.View
{
    using System;
    using Trading.Interface;

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

        public void Clear()
        {
            Console.Clear();
        }
    }
}
