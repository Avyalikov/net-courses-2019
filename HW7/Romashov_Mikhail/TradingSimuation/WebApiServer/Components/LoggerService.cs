using log4net;
using log4net.Config;
using System;
using WebApiServer.Interfaces;

namespace WebApiServer.Components
{
    public class LoggerService : ILogger
    {
        private readonly ILog logger;

        public LoggerService()
        {
            this.logger = LogManager.GetLogger("Logger");
        }

        public void Error(Exception ex)
        {
            logger.Error(ex);
        }
        public void InitLogger()
        {
            XmlConfigurator.Configure();
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
    }
}