namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System;
    using TradingSoftware.Core.Services;

    public class ReduceAssetsRandomClientStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IClientManager clientManager;

        private Random random = new Random();

        public ReduceAssetsRandomClientStrategy(ILoggerService loggerService, IClientManager clientManager)
        {
            this.loggerService = loggerService;
            this.clientManager = clientManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ReduceAssetsRandomClient)
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
                this.clientManager.ChangeBalance(clientID, -100000);
            });
            this.loggerService.Info($"Assets of client with id = {clientID} was reduced");
        }
    }
}