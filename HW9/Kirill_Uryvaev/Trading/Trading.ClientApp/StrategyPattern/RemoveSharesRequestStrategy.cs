using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ClientApp.StrategyPattern
{
    class RemoveSharesRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("removeshares");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            if (splittedUserInpit.Length < 3)
            {
                return "Not enough parameters";
            }
            int clientId;
            if (!int.TryParse(splittedUserInpit[1], out clientId))
            {
                return "Cannot parse ID";
            }
            int shareId;
            if (!int.TryParse(splittedUserInpit[2], out shareId))
            {
                return "Cannot parse ID";
            }
            requestSender.PostRemoveShare(new int[] {clientId, shareId }, out answer);
            return answer;
        }
    }
}
