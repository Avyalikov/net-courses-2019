namespace ConsoleCanvas
{
    using System;
    using ConsoleCanvas.Interfaces;

    public class KeyboardManager : IKeyboardManager
    {
        private const bool DontShow = true;

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(DontShow);
        }
    }
}