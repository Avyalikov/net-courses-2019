using TradingApp.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Trading.Core.Services;
using Trading.Core.Modifiers;

namespace TradingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = new Logger(log4net.LogManager.GetLogger("Logger"));

            using (ExchangeContext db = new ExchangeContext())
            {
               
                
               
                logger.Info("Trading is started");
               
                logger.Info("Trading is finished");
               
            }
        }
    }
}
