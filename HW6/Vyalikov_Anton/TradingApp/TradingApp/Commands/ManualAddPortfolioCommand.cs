namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class ManualAddPortfolioCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly IConsoleApp consoleApp;

        public ManualAddPortfolioCommand(ILogger logger, IConsoleApp consoleApp)
        {
            this.logger = logger;
            this.consoleApp = consoleApp;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddPortfolio)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.consoleApp.RegisterPortfolio();
            });

            this.logger.WriteMessage("Portfolio manually added to portfolio base.");
        }
    }
}
