using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class UpdateSharesStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public UpdateSharesStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.sharesUpdate)
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
