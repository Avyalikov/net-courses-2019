namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;

    class GetAllClientsCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ITradeTable tradeTable;
        private readonly IClientService clientService;

        public GetAllClientsCommand(ILogger logger, ITradeTable tradeTable, IClientService clientService)
        {
            this.logger = logger;
            this.tradeTable = tradeTable;
            this.clientService = clientService;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.GetAllClients)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                this.tradeTable.Draw(clientService.GetAllClients());
            });

            this.logger.WriteMessage("Got all clients from client base.");
        }
    }
}
