namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System.Linq;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.Core.Services;

    public class ShowClientsWithOrangeStatusStrategy : ICommandStrategy
    {
        private readonly ILoggerService loggerService;
        private readonly IClientManager clientManager;
        private readonly ITableDrawer tableDrawer;
        private readonly IOutputDevice outputDevice;

        public ShowClientsWithOrangeStatusStrategy(ILoggerService loggerService, IClientManager clientManager, ITableDrawer tableDrawer, IOutputDevice outputDevice)
        {
            this.loggerService = loggerService;
            this.clientManager = clientManager;
            this.tableDrawer = tableDrawer;
            this.outputDevice = outputDevice;
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
            this.loggerService.RunWithExceptionLogging(() =>
            {
                outputDevice.Clear();
                outputDevice.WriteLine("Clients with 'Orange' status:");
                tableDrawer.Show(clientManager.GetAllClients().Where(c => c.Balance == 0));
            });
            this.loggerService.Info($"Readed all clients with 'Orange' status (balance == 0)");
        }
    }
}