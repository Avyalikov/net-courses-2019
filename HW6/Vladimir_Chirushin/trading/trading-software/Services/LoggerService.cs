namespace trading_software.Services
{
    using log4net;
    using System;

    public class LoggerService : ILoggerService
    {
        private ILog logger;

        public LoggerService()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.logger = LogManager.GetLogger("SampleLogger");
        }

        public void SetUpLogger(ILog logger)
        {
            this.logger = logger;
        }


        public void Error(Exception ex)
        {
            logger.Error(ex);
        }

        public void Info(string message)
        {
            logger.Info(message);
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
