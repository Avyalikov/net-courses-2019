namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    public class StopSimulationRandomTransactionStrategy : ICommandStrategy
    {
        private readonly ITimeManager timeManager;
        private readonly ILoggerService loggerService;

        public StopSimulationRandomTransactionStrategy(ITimeManager timeManager, ILoggerService loggerService)
        {
            this.timeManager = timeManager;
            this.loggerService = loggerService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.StopSimulationWithRandomTransactions)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() => this.timeManager.StopRandomTransactionThread());
            this.loggerService.Info("Simulation with random transactions was stoped.");
        }
    }
}