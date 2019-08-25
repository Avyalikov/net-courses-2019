namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using TradingSoftware.Core.Services;

    public class ReadAllClientsStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly ITableDrawer tableDrawer;
        private readonly IClientManager clientManager;

        public ReadAllClientsStrategy(ILoggerService loggerService, ITableDrawer tableDrawer, IClientManager clientManager)
        {
            this.loggerService = loggerService;
            this.tableDrawer = tableDrawer;
            this.clientManager = clientManager;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ReadAllClients)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
            {
                this.tableDrawer.Show(clientManager.GetAllClients());
            });

            this.loggerService.Info("Readed all Clients from ClientBase");
        }
    }
}