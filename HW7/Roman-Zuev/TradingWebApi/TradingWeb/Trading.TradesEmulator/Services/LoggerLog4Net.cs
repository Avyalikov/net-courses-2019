using log4net;
using log4net.Config;

namespace Trading.TradesEmulator.Services
{
    public class LoggerLog4Net
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
    }
}
