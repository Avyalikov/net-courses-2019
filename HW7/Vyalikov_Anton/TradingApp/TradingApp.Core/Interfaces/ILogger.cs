namespace TradingApp.Core.Interfaces
{
    using System;
    public interface ILogger
    {
        void InitLogger();
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);
        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);
        void WriteMessage(string message);
        void WriteWarning(string message);
        void WriteError(string message);
    }
}
