namespace TradingApiClient.Devices
{
    public interface IOutputDevice
    {
        void WriteLine(string outputString);

        void Clear();
    }
}
