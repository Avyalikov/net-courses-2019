namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Services;

    public class ManualAddTransactionStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IUserInteractionManager userInteractionManager;

        public ManualAddTransactionStrategy(ILoggerService loggerService, IUserInteractionManager userInteractionManager)
        {
            this.loggerService = loggerService;
            this.userInteractionManager = userInteractionManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddTransaction)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
            {
                this.userInteractionManager.ManualAddTransaction();
            });

            this.loggerService.Info("Manual added share to ShareBase");
        }
    }
}