using System;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class DealMakeStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public DealMakeStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.transactions)
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
