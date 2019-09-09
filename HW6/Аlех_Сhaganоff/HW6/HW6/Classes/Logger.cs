using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW6.Interfaces;
using log4net;
using log4net.Config;

namespace HW6
{
    public class Logger : ILogger
    {
        private ILog log = LogManager.GetLogger("LOGGER");

        public ILog Log
        {
            get { return log; }
        }

        public void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public Logger()
        {
            InitLogger();
        }

        public void Write(string text)
        {
            Log.Info(text);
        }
    }
}
