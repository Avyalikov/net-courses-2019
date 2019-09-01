using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServer.Interfaces
{
    public interface ILogger
    {
        void Error(Exception ex);
        void InitLogger();
        void Info(string message);
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);

    }
}