namespace TradingApiClient.Devices
{
    using System;

    public interface IInputDevice
    {
        string ReadLine();

        ConsoleKeyInfo ReadKey();
    }
}
