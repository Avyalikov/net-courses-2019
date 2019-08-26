namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    public class StartSimulationRandomTransactionStrategy : ICommandStrategy
    {
        private readonly ITimeManager timeManager;
        private readonly ILoggerService loggerService;

        public StartSimulationRandomTransactionStrategy(ITimeManager timeManager, ILoggerService loggerService)
        {
            this.timeManager = timeManager;
            this.loggerService = loggerService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.StartSimulationWithRandomTransactions)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() => this.timeManager.StartRandomTransactionThread());
            this.loggerService.Info("Simulation with random transactions was started.");
        }
    }
}