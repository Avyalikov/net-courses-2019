using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApiClient.Services.CommandStrategy
{
    public interface ICommandStrategy
    {
        bool CanExecute(Command command);

        void Execute();
    }
}
