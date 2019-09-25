namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Services;

    public class ManualAddBlockOfSharesStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IUserInteractionManager userInteractionManager;

        public ManualAddBlockOfSharesStrategy(ILoggerService loggerService, IUserInteractionManager userInteractionManager)
        {
            this.loggerService = loggerService;
            this.userInteractionManager = userInteractionManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddShares)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
            {
                this.userInteractionManager.ManualAddNewBlockOfShare();
            });

            this.loggerService.Info("Manual added share to ShareBase");
        }
    }
}