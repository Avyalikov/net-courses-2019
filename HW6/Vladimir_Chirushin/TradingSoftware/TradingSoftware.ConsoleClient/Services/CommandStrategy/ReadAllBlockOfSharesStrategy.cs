namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Core.Services;

    public class ReadAllBlockOfSharesStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly ITableDrawer tableDrawer;
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public ReadAllBlockOfSharesStrategy(ILoggerService loggerService, ITableDrawer tableDrawer, IBlockOfSharesManager blockOfSharesManager)
        {
            this.loggerService = loggerService;
            this.tableDrawer = tableDrawer;
            this.blockOfSharesManager = blockOfSharesManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ReadAllBlockOfShares)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
                {
                    this.tableDrawer.Show(blockOfSharesManager.GetAllBlockOfShares());
                });

            this.loggerService.Info("Readed all BlockOfShares from BlockOfSharesBase");
        }
    }
}