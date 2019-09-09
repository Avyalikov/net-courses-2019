namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class DBInitiateCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly IDBInitializer dBInitializer;

        public DBInitiateCommand(ILogger logger, IDBInitializer dBInitializer)
        {
            this.logger = logger;
            this.dBInitializer = dBInitializer;
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
            this.logger.RunWithExceptionLogging(() => this.dBInitializer.GenerateDB());
            this.logger.WriteMessage("DataBase was initiated with default clients, stocks and BlocksOfShares");
        }
    }
}
