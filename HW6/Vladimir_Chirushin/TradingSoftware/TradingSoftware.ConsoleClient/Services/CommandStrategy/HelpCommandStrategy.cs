namespace TradingSoftware.ConsoleClient.Services.CommandStrategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingSoftware.ConsoleClient.Devices;

    public class HelpCommandStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public HelpCommandStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.Help)
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
