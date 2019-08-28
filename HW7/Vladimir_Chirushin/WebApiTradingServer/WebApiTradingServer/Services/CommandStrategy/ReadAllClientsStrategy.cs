namespace WebApiTradingServer.Services.CommandStrategy
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class ReadAllClientsStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IClientManager clientManager;

        public ReadAllClientsStrategy(ILoggerService loggerService, IClientManager clientManager)
        {
            this.loggerService = loggerService;
            this.clientManager = clientManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.getClients)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            IEnumerable<Client> clients;
            this.loggerService.RunWithExceptionLogging(() =>
            {
                clients = clientManager.GetAllClients();
            });
            this.loggerService.Info("Readed all Clients from ClientBase");

            //return clients;
        }
    }
}