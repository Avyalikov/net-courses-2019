namespace WebApiTradingServer.Services
{
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebApiTradingServer.Services.CommandStrategy;

    public enum Command
    {
        getClients,
        addClient,
        updateClient,
        removeClient,

        getSharesForClient,
        addShareForClient,
        updateShareForClient,
        removeShareForClient,

        getClientStatus,
        getTransactionsForClient,
        makeTransaction
    }

    public class CommandParser : ICommandParser
    {
        private readonly ILoggerService loggerService;

        private Command command;

        public CommandParser(ILoggerService loggerService)
        {
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
                this.loggerService.Info($"Engine get unvalid command: {commandString}.");
            }
        }
    }
}
