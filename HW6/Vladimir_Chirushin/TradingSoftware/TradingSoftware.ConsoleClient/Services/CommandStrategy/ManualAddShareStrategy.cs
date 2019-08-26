namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Services;

    public class ManualAddShareStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IUserInteractionManager userInteractionManager;

        public ManualAddShareStrategy(ILoggerService loggerService, IUserInteractionManager userInteractionManager)
        {
            this.loggerService = loggerService;
            this.userInteractionManager = userInteractionManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ManualAddStock)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
            {
                this.userInteractionManager.ManualAddShare();
            });

            this.loggerService.Info("Manual added share to ShareBase");
        }
    }
}