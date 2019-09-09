namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class GetAllPortfoliosCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ITradeTable tradeTable;
        private readonly IPortfoliosService portfoliosService;

        public GetAllPortfoliosCommand(ILogger logger, ITradeTable tradeTable, IPortfoliosService portfoliosService)
        {
            this.logger = logger;
            this.tradeTable = tradeTable;
            this.portfoliosService = portfoliosService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetAllPortfolios)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.tradeTable.Draw(portfoliosService.GetAllPortfolios());
            });

            this.logger.WriteMessage("Got all portfolios from portfolios base.");
        }
    }
}
