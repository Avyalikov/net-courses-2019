using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core
{
    public interface ILogger
    {
        void InitLogger();
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);
        void WriteInfo(string message);
        void WriteWarn(string message);
        void WriteError(string message);
    }
}
