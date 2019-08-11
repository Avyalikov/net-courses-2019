using System;

namespace trading_software
{
    public class InputDevice : IInputDevice
    {
        const bool readKeySilently = true;
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(readKeySilently);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
