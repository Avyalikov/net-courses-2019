using System;

namespace CreateDoorsAndLevels.Modules
{
    /* Using console for interaction between user and program
     */
    class ConsoleInputOutputDevice : Interfaces.IInputOutputDevice
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public void WriteLineOutput(string dataToOutPut)
        {
            Console.WriteLine(dataToOutPut);
        }

        public void WriteOutput(string dataToOutPut)
        {
            Console.Write(dataToOutPut);
        }
    }
}
