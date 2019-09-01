namespace TradingApiClient.Services.CommandStrategy
{
    using System.Collections.Generic;
    using StructureMap;
    using TradingApiClient.Devices;

    public class CommandStrategyRegistry : Registry
    {
        public CommandStrategyRegistry()
        {
            this.For<IOutputDevice>().Use<OutputDevice>();
            this.For<IInputDevice>().Use<InputDevice>();
            this.For<IHttpRequestManager>().Use<HttpRequestManager>();
            this.For<ICommandParser>().Use<CommandParser>();

            this.For<ICommandStrategy>().Add<HelpCommandStrategy>();
            this.For<ICommandStrategy>().Add<AddClientsStrategy>();
            this.For<ICommandStrategy>().Add<AddSharesStrategy>();
            this.For<ICommandStrategy>().Add<BalancesStrategy>();
            this.For<ICommandStrategy>().Add<DealMakeStrategy>();
            this.For<ICommandStrategy>().Add<ReadClientsStrategy>();
            this.For<ICommandStrategy>().Add<ReadSharesStrategy>();
            this.For<ICommandStrategy>().Add<RemoveClientsStrategy>();
            this.For<ICommandStrategy>().Add<RemoveSharesStrategy>();
            this.For<ICommandStrategy>().Add<TransacactionsStrategy>();
            this.For<ICommandStrategy>().Add<UpdateClientsStrategy>();
            this.For<ICommandStrategy>().Add<UpdateSharesStrategy>();

            this.For<IEnumerable<ICommandStrategy>>().Use(x => x.GetAllInstances<ICommandStrategy>());
        }
    }
}