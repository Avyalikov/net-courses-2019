namespace TradingApp.View.Logger
{
    using log4net;
    using log4net.Config;
    static class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");
    }
}