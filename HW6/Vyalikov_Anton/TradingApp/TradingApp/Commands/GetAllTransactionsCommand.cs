namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class GetAllTransactionsCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ITradeTable tradeTable;
        private readonly ITransactionsService transactService;

        public GetAllTransactionsCommand(ILogger logger, ITradeTable tradeTable, ITransactionsService transactService)
        {
            this.logger = logger;
            this.tradeTable = tradeTable;
            this.transactService = transactService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetAllTransactions)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.tradeTable.Draw(transactService.GetAllTransactions());
            });

            this.logger.WriteMessage("Got all transactions from transaction base.");
        }
    }
}
