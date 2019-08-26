namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class InitiateDBCommand : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IDataBaseInitializer dataBaseInitializer;

        public InitiateDBCommand(ILoggerService loggerService, IDataBaseInitializer dataBaseInitializer)
        {
            this.loggerService = loggerService;
            this.dataBaseInitializer = dataBaseInitializer;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.InitiateDB)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() => this.dataBaseInitializer.Initiate());
            this.loggerService.Info("DataBase was initiated with default clients, stocks and BlocksOfShares");
        }
    }
}