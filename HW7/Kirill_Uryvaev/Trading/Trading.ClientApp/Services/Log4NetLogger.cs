using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.ClientApp
{
    class Log4NetLogger : ILogger
    {
        private readonly ILog logger;

        public Log4NetLogger(bool isTradeLog)
        {
            if (isTradeLog)
            {
                logger = LogManager.GetLogger("TradeLogger");
            }
            else
            {
                logger = LogManager.GetLogger("Logger");
            }
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
                logger.Error(ex);

                if (isSilent)
                {
                    return;
                }

                throw;
            }
        }

        public void WriteError(string message)
        {
            logger.Error(message);
        }

        public void WriteInfo(string message)
        {
            logger.Info(message);
        }

        public void WriteWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
