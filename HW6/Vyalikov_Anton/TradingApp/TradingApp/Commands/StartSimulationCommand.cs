namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class StartSimulationCommand : ICommand
    {
        private readonly ITradingTimer timer;
        private readonly ILogger logger;

        public StartSimulationCommand(ITradingTimer timer, ILogger logger)
        {
            this.timer = timer;
            this.logger = logger;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.StartSimulation)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() => this.timer.StartRandomTrading());
            this.logger.WriteMessage("Trading simulation was started.");
        }
    }
}
