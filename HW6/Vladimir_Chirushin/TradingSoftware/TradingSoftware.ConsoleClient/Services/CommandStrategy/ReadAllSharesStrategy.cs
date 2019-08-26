namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Core.Services;

    public class ReadAllSharesStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly ITableDrawer tableDrawer;
        private readonly IShareManager shareManager;

        public ReadAllSharesStrategy(ILoggerService loggerService, ITableDrawer tableDrawer, IShareManager shareManager)
        {
            this.loggerService = loggerService;
            this.tableDrawer = tableDrawer;
            this.shareManager = shareManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ReadAllShares)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
                {
                    this.tableDrawer.Show(shareManager.GetAllShares());
                });
            this.loggerService.Info("Readed all Shares from ShareBase");
        }
    }
}