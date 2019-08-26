namespace TradingSoftware.ConsoleClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StructureMap;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.ConsoleClient.Services.CommandStrategy;

    public enum Command
    {
        Help,
        ManualAddClient,
        ManualAddStock,
        ManualAddTransaction,
        ManualAddShares,
        ReadAllClients,
        ReadAllBlockOfShares,
        ReadAllTransactions,
        ReadAllShares,
        MakeRandomTransaction,
        InitiateDB,
        BankruptRandomClient,
        ShowOrangeClients,
        ShowBlackClients,
        ReduceAssetsRandomClient,
        StartSimulationWithRandomTransactions,
        StopSimulationWithRandomTransactions
    }

    public class CommandParser : ICommandParser
    {
        private readonly IOutputDevice outputDevice;
        private readonly ILoggerService loggerService;

        private Command command;

        public CommandParser(
            IOutputDevice outputDevice,
            ILoggerService loggerService)
        {
            this.outputDevice = outputDevice;
            this.loggerService = loggerService;
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