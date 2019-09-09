// <copyright file="ILogger.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ILogger description
    /// </summary>
    public interface ILogger
    {
        void RunWithExceptionLogging(Action actionToRun, bool isSilent = false);
        T RunWithExceptionLogging<T>(Func<T> functionToRun, bool isSilent = false);
        void Info(string message);
        void Error(Exception ex);
    }
}
