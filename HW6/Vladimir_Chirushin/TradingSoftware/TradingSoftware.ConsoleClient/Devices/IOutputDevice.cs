namespace TradingSoftware.ConsoleClient.Devices
{
    public interface IOutputDevice
    {
        void WriteLine(string outputString);

        void Clear();
    }
}
