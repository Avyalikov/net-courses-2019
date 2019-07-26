namespace GameWhichCanDraw.Components
{
    using System;

    /* Using console for interaction between user and program
     */
    internal class ConsoleInputOutputDevice : Interfaces.IInputOutputDevice
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void SetPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void WriteLineOutput(string dataToOutPut)
        {
            Console.WriteLine(dataToOutPut);
        }

        public void WriteOutput(string dataToOutPut)
        {
            Console.Write(dataToOutPut);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
