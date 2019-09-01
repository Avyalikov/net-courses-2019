namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class ManualAddClientCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly IConsoleApp consoleApp;

        public ManualAddClientCommand(ILogger logger, IConsoleApp consoleApp)
        {
            this.logger = logger;
            this.consoleApp = consoleApp;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddClient)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.consoleApp.RegisterClient();
            });

            this.logger.WriteMessage("Client manually added to client base.");
        }
    }
}
