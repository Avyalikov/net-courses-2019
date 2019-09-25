namespace TradingApp.Core
{
    using log4net;
    using log4net.Config;

    public static class Logger
    {
        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
        /// <summary>
        /// It is used for logging in console
        /// </summary>
        public static ILog ConsoleLogger { get; } = LogManager.GetLogger("ConsoleLogger");
        /// <summary>
        /// It is used for logging actions
        /// </summary>
        public static ILog FileLogger { get; } = LogManager.GetLogger("FileLogger");
        /// <summary>
        /// It is used for logging transaction history
        /// </summary>
        public static ILog FileTransactionLogger { get; } = LogManager.GetLogger("FileTransactionLogger");
    }
}
