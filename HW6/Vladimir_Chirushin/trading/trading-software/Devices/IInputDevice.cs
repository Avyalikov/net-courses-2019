namespace trading_software
{
    using System;
    public interface IInputDevice
    {
        string ReadLine();
        ConsoleKeyInfo ReadKey();
    }
}