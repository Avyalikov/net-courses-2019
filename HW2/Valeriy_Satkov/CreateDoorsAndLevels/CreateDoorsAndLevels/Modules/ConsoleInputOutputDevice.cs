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

        public void WriteOutput(string dataToOutPut)
        {
            Console.WriteLine(dataToOutPut);
        }

        public void WriteSomeOutput(string dataToOutPut)
        {
            Console.Write(dataToOutPut);
        }
    }
}
