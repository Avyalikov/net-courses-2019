namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Services;

    public class ManualAddClientStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IUserInteractionManager userInteractionManager;

        public ManualAddClientStrategy(ILoggerService loggerService, IUserInteractionManager userInteractionManager)
        {
            this.loggerService = loggerService;
            this.userInteractionManager = userInteractionManager;
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
            this.loggerService.RunWithExceptionLogging(() =>
            {
                this.userInteractionManager.ManualAddClient();
            });

            this.loggerService.Info("Manual added client to ClientBase");
        }
    }
}