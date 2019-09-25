namespace DrawGame
{
    using System;
    using DrawGame.Interfaces;

    internal class ConsoleIO : IInputOutput
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }
    }
}
