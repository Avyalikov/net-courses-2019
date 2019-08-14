using trading_software.Services;
using StructureMap;

namespace trading_software
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            ILoggerService logger = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            var container = new Container(new TradingSoftwareRegistry());
            var tradingEngine = container.GetInstance<ITradingEngine>();

            tradingEngine.Run();
        }
    }
}
