using System;

namespace DoorsAndLevelsAfterRefactoring
{
    public class ConsoleInputOutputDevice : IInputOutputDevice
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
            Console.WriteLine(dataToOutput);
        }
    }
}
