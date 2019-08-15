namespace TradingData
{
    using log4net;
    using log4net.Config;

    internal static class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");
    }
}
