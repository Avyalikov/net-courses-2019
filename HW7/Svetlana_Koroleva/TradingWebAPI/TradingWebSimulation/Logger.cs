// <copyright file="Logger.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingWebSimulation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using log4net.Config;

    /// <summary>
    /// Logger description
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILog logger;
        public Logger(ILog logger)
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
