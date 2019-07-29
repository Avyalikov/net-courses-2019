namespace ConsoleDraw.Provider
{
    using System;
    using ConsoleDraw.Interfaces;

    class ConsoleIO : IInputOutputDevice
    {
        public void Clear()
        {
            Console.Clear();
        }

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

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(0 + x, 0 + y);
        }
    }
}
