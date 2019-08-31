using System;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class BalancesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public BalancesStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.balances)
            {
                return true;
            }

            return false;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
