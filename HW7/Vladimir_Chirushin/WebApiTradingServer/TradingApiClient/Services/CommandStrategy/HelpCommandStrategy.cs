namespace TradingApiClient.Services.CommandStrategy
{
    using System;
    using TradingApiClient.Devices;

    public class HelpCommandStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public HelpCommandStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.help)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            foreach (Command command in Enum.GetValues(typeof(Command)))
            {
                this.outputDevice.WriteLine(command.ToString());
            }
        }
    }
}