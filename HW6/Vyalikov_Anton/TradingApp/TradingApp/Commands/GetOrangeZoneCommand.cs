namespace TradingApp.Commands
{
    using Interfaces;
    using Services;
    using TradingApp.Core.Interfaces;
    using System.Linq;

    class GetOrangeZoneCommand : ICommand
    {
        private readonly ILogger logger;
        private readonly ITradeTable tradeTable;
        private readonly IClientService clientService;
        private readonly IInputOutputModule ioDevice;

        public GetOrangeZoneCommand(ILogger logger, ITradeTable tradeTable, IClientService clientService, IInputOutputModule ioDevice)
        {
            this.logger = logger;
            this.tradeTable = tradeTable;
            this.clientService = clientService;
            this.ioDevice = ioDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ShowOrangeClients)
            {
                return true;
            }
            return false;
        }

        public void Execute()
        {
            this.logger.RunWithExceptionLogging(() =>
            {
                ioDevice.Clear();
                ioDevice.WriteOutput("Clients with 'Orange' status:");
                tradeTable.Draw(clientService.GetAllClients().Where(c => c.Balance == 0));
            });

            this.logger.WriteMessage("Got all clients with 'Orange' status (balance 0 0).");
        }
    }
}