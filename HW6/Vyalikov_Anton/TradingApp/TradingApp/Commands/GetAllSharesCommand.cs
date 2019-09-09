namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class GetAllSharesCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ITradeTable tradeTable;
        private readonly ISharesService sharesService;

        public GetAllSharesCommand(ILogger logger, ITradeTable tradeTable, ISharesService sharesService)
        {
            this.logger = logger;
            this.tradeTable = tradeTable;
            this.sharesService = sharesService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetAllShares)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.tradeTable.Draw(sharesService.GetAllShares());
            });

            this.logger.WriteMessage("Got all shares from shares base.");
        }
    }
}
