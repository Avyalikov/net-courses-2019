using System;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class AddSharesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public AddSharesStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.sharesAdd)
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
