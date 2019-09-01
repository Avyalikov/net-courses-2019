using Trading.TradesEmulator.Services;
namespace Trading.TradesEmulator
{
    class Program
    {
        static void Main()
        {
            var trades = new TradesEmulator(new LoggerLog4Net());
            trades.Run();
        }
    }
}
