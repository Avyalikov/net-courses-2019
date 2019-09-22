namespace TradeSimulator.Server
{
    using log4net;
    using TradeSimulator.Core;

    public class Logger : ILogger
    {
        private readonly ILog logger;
        public Logger()
        {
            this.logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

    }
}
