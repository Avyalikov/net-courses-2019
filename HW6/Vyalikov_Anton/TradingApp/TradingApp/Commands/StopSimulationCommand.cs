namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class StopSimulationCommand : ICommand
    {
        private readonly ITradingTimer timer;
        private readonly ILogger logger;

        public StopSimulationCommand(ITradingTimer timer, ILogger logger)
        {
            this.timer = timer;
            this.logger = logger;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.StopSimulation)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() => this.timer.StopRandomTrading());
            this.logger.WriteMessage("Trading simulation was stopped.");
        }
    }
}