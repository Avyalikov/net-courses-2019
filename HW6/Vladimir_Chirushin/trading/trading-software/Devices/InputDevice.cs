namespace trading_software
{
    using System;

    public class InputDevice : IInputDevice
    {
        const bool ReadKeySilently = true;
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(ReadKeySilently);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}