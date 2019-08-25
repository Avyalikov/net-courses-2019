namespace TradingApp.Core.Logger
{
    using log4net;
    using log4net.Config;
    public static class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");
    }
}