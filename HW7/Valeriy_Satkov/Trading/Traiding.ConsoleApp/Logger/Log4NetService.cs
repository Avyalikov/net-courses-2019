namespace Traiding.ConsoleApp.Logger
{
    using System;
    using log4net;
    using log4net.Config;

    public class Log4NetService : ILoggerService
    {
        private readonly ILog logger;

        public Log4NetService(bool isItTrading)
        {
            if (isItTrading)
            {
                this.logger = LogManager.GetLogger("TraidingLOGGER");
            }
            else
            {
                this.logger = LogManager.GetLogger("LOGGER");
            }
        }

        public void Error(Exception ex)
        {
            logger.Error(ex);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public ILog Log
        {
            get { return logger; }
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
                logger.Error("ERROR: ", ex);

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
                logger.Error("ERROR: ", ex);

                if (isSilent)
                {
                    return default(T);
                }

                throw;
            }
        }
    }
}
