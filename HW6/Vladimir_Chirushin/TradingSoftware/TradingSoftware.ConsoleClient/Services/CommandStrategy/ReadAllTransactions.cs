namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Core.Services;

    public class ReadAllTransactions : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly ITableDrawer tableDrawer;
        private readonly ITransactionManager transactionManager;

        public ReadAllTransactions(ILoggerService loggerService, ITableDrawer tableDrawer, ITransactionManager transactionManager)
        {
            this.loggerService = loggerService;
            this.tableDrawer = tableDrawer;
            this.transactionManager = transactionManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ReadAllTransactions)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
                {
                    this.tableDrawer.Show(transactionManager.GetAllTransactions());
                });
            this.loggerService.Info("Readed all transactions from TransactionBase");
        }
    }
}