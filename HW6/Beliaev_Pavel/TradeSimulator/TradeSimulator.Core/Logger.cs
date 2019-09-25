namespace TradeSimulator.Core
{
    using log4net;

    public class Logger : ILogger
    {
        private readonly ILog logger;
        public Logger(ILog logger)
        {
            this.logger = logger;
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

    }
}
