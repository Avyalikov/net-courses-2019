namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System;
    using TradingSoftware.Core.Services;

    public class BankruptRandomClientStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IClientManager clientManager;

        private Random random = new Random();

        public BankruptRandomClientStrategy(ILoggerService loggerService, IClientManager clientManager)
        {
            this.loggerService = loggerService;
            this.clientManager = clientManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.BankruptRandomClient)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            int clientID = this.random.Next(1, this.clientManager.GetNumberOfClients());
            this.loggerService.RunWithExceptionLogging(() =>
            {
                clientManager.ChangeBalance(
                    clientID, (-1) * clientManager.GetClientBalance(clientID));
            });
            this.loggerService.Info($"Client with id = {clientID} was bankrupt");
        }
    }
}