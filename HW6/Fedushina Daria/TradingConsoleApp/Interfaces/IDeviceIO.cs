
namespace TradingConsoleApp.Interfaces
{
   public interface IDeviceIO
    {
        void WriteOutput(string OutputData);
        string ReadOutput();
    }
}