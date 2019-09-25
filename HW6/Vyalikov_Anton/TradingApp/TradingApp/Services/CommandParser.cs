namespace TradingApp.Services
{
    using Commands;
    using Interfaces;
    using TradingApp.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StructureMap;

    public enum Command
    {
        Help,
        ManualAddClient,
        ManualAddShare,
        ManualAddPortfolio,
        GetAllClients,
        GetAllShares,
        GetAllTransactions,
        GetAllPortfolios,
        InitiateDB,
        ShowOrangeClients,
        ShowBlackClients,
        StartSimulation,
        StopSimulation
    }
    class CommandParser : ICommandParser
    {
        private readonly IInputOutputModule ioModule;
        private readonly ILogger logger;

        private Command command;

        public CommandParser(
            IInputOutputModule ioModule,
            ILogger logger)
        {
            this.ioModule = ioModule;
            this.logger = logger;
        }

        public void Parse(string commandstr)
        {
            var container = new Container(new CommandRegistry());
            var commands = container.GetInstance<IEnumerable<ICommand>>();

            if (Enum.TryParse(commandstr, true, out this.command))
            {
                var comm = commands.FirstOrDefault(s => s.CanExecute(this.command));
                if (comm != null)
                {
                    comm.Execute();
                }
            }
            else
            {
                this.ioModule.WriteOutput("Incorrect command");
            }
        }
    }
}
