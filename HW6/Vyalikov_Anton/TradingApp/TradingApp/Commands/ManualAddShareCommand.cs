namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class ManualAddShareCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly IConsoleApp consoleApp;

        public ManualAddShareCommand(ILogger logger, IConsoleApp consoleApp)
        {
            this.logger = logger;
            this.consoleApp = consoleApp;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddShare)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.consoleApp.RegisterShare();
            });

            this.logger.WriteMessage("Share manually added to shares base.");
        }
    }
}
