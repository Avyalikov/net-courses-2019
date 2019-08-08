using System;

namespace EntityForNorthwind.Services
{
    public interface ILoggerService
    {
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);

        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);

        void Info(string message);

        void Error(Exception ex);
    }
}
