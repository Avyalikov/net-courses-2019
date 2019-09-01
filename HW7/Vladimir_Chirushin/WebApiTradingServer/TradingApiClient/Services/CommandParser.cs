namespace TradingApiClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StructureMap;
    using TradingApiClient.Devices;
    using TradingApiClient.Services.CommandStrategy;

    public enum Command
    {
        help,
        clients,
        clientsAdd,
        clientsUpdate,
        clientsRemove,
        shares,
        sharesAdd,
        sharesUpdate,
        sharesRemove,
        balances,
        transactions,
        dealMake
    }

    public class CommandParser : ICommandParser
    {
        private readonly IOutputDevice outputDevice;

        private Command command;

        public CommandParser(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public void Parse(string commandString)
        {
            var container = new Container(new CommandStrategyRegistry());
            var strategies = container.GetInstance<IEnumerable<ICommandStrategy>>();

            if (Enum.TryParse(commandString, true, out this.command))
            {
                var strategy = strategies.FirstOrDefault(s => s.CanExecute(this.command));
                if (strategy != null)
                {
                    strategy.Execute();
                }
            }
            else
            {
                this.outputDevice.WriteLine("command not recognized");
            }
        }
    }
}
