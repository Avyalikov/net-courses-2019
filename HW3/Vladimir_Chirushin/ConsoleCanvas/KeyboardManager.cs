using ConsoleCanvas.Interfaces;
using System;

namespace ConsoleCanvas
{
    public class KeyboardManager : IKeyboardManager
    {
        private const bool dontShow = true;

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(dontShow);
        }
    }
}