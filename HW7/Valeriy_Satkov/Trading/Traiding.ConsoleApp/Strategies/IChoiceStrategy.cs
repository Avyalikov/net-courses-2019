namespace Traiding.ConsoleApp.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Logger;

    interface IChoiceStrategy
    {
        bool CanExecute(string userChoice);
        string Run(RequestSender requestSender, ILoggerService loggerService);
    }
}
