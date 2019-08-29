namespace TradingApp.Services
{
    using TradingApp.Core.Interfaces;
    using log4net;
    using log4net.Config;
    using System;

    class Logger : ILogger
    {
        private readonly ILog logger;

        public Logger()
        {

            this.logger = LogManager.GetLogger("Logger");
        }

        public void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public void RunWithExceptionLogging(Action actionToRun, bool isSilent = false)
        {
            try
            {
                actionToRun();
            }
            catch (Exception ex)
            {
                this.logger.Error("ERROR: ", ex);

                if (isSilent)
                {
                    return;
                }

                throw;
            }
        }

        public T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false)
        {
            try
            {
                return functionToRun();
            }
            catch (Exception ex)
            {
                this.logger.Error("ERROR: ", ex);

                if (isSilent)
                {
                    return default(T);
                }

                throw;
            }
        }

        public void WriteError(string message)
        {
            logger.Error(message);
        }

        public void WriteMessage(string message)
        {
            logger.Info(message);
        }

        public void WriteWarning(string message)
        {
            logger.Warn(message);
        }
    }
}

