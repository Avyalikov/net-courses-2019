namespace TradingSoftware.ConsoleClient.Services
{
    using System;
    using log4net;

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
            this.logger.Error(ex);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
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
    }
}
