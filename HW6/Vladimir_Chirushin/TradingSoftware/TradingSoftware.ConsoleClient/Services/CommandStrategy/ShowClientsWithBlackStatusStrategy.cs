namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System.Linq;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.Core.Services;

    public class ShowClientsWithBlackStatusStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IClientManager clientManager;
        private readonly ITableDrawer tableDrawer;
        private readonly IOutputDevice outputDevice;

        public ShowClientsWithBlackStatusStrategy(ILoggerService loggerService, IClientManager clientManager, ITableDrawer tableDrawer, IOutputDevice outputDevice)
        {
            this.loggerService = loggerService;
            this.clientManager = clientManager;
            this.tableDrawer = tableDrawer;
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.ShowBlackClients)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            this.loggerService.RunWithExceptionLogging(() =>
            {
                outputDevice.Clear();
                outputDevice.WriteLine("Clients with 'Black' status:");
                tableDrawer.Show(clientManager.GetAllClients().Where(c => c.Balance < 0));
            });
            this.loggerService.Info($"Readed all clients with 'Black' status (balance < 0)");
        }
    }
}
