using System;

namespace ConsoleCanvas
{
    public class KeyboardManager : IKeyboardManager
    {
        private Boolean dontShow = true;
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(dontShow);
        }
    }
}