using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingSimulator.ConsoleApp.Components
{
    //public static class Logger 
    //{
    //    public static ILog MainLog { get; } = LogManager.GetLogger("Logger");
    //    public static ILog TradeLog { get; } = LogManager.GetLogger("TradeLogger");

    //    public static void InitLogger()
    //    {
    //        XmlConfigurator.Configure();
    //    }

    //    public static void RunWithExceptionLogging(Action actionToRun, bool isSilent = false)
    //    {
    //        try
    //        {
    //            actionToRun();
    //        }
    //        catch (Exception ex)
    //        {
    //            MainLog.Error(ex);

    //            if (isSilent)
    //            {
    //                return;
    //            }
    //            throw;
    //        }
    //    }
    //}
}
