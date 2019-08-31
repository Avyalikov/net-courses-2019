using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApiClient.Devices;

namespace TradingApiClient.Services.CommandStrategy
{

    public class RemoveClientsStrategy : ICommandStrategy
    {
        private readonly IOutputDevice outputDevice;

        public RemoveClientsStrategy(IOutputDevice outputDevice)
        {
            this.outputDevice = outputDevice;
        }

        public bool CanExecute(Command command)
        {
            if (command == Command.clientsRemove)
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
