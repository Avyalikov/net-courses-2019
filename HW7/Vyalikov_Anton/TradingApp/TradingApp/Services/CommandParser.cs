namespace TradingApp.Services
{
    using Commands;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StructureMap;

    public enum Command
    {
        Help,
        ManualAddClient,
        ManualAddPortfolio,
        RemoveClient,
        RemovePortfolio,
        UpdateClient,
        UpdatePortfolio,
        GetBalance,
        MakeDeal,
        GetClients,
        GetPortfolios,
        GetTransactions
    }
    class CommandParser : ICommandParser
    {
        private readonly IInputOutputModule ioModule;

        private Command command;

        public CommandParser(IInputOutputModule ioModule)
        {
            this.ioModule = ioModule;
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
