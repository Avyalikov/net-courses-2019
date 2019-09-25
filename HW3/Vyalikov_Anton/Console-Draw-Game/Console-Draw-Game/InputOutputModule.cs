namespace Console_Draw_Game
{
    using System;

    class InputOutputModule : Interfaces.IInputOutputModule
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string outputData)
        {
            Console.WriteLine(outputData);
        }

        public void SetPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
