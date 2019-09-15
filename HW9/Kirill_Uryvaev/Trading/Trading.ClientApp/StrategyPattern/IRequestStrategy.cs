using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ClientApp.StrategyPattern
{
    interface IRequestStrategy
    {
        bool CanExicute(string command);
        string Run(string[] splittedUserInpit, RequestSender requestSender);
    }
}
