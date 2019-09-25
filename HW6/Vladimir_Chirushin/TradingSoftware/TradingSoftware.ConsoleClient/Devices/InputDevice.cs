namespace TradingSoftware.ConsoleClient.Devices
{
    using System;

    public class InputDevice : IInputDevice
    {
        private const bool ReadKeySilently = true;

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