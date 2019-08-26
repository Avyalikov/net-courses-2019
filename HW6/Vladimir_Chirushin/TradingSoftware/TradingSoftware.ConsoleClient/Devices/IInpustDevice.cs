namespace TradingSoftware.ConsoleClient.Devices
{
    using System;

    public interface IInputDevice
    {
        string ReadLine();

        ConsoleKeyInfo ReadKey();
    }
}
