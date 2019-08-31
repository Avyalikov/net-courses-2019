using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.ClientApp.StrategyPattern
{
    class AddSharesRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("addshares");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            if (splittedUserInpit.Length < 5)
            {
                return "Not enough parameters";
            }
            int clientID;
            if (!int.TryParse(splittedUserInpit[1], out clientID))
            {
                return "Cannot parse ID";
            }
            int shareID;
            if (!int.TryParse(splittedUserInpit[2], out shareID))
            {
                return "Cannot parse ID";
            }
            decimal cost;
            if (!decimal.TryParse(splittedUserInpit[3], out cost))
            {
                return "Cannot parse cost";
            }
            int amount;
            if (!int.TryParse(splittedUserInpit[4], out amount))
            {
                return "Cannot parse amount";
            }
            ClientsSharesInfo sharesInfo = new ClientsSharesInfo
            {
                ClientID = clientID,
                ShareID = shareID,
                CostOfOneShare = cost,
                Amount = amount
            };
            requestSender.PostAddShare(sharesInfo, out answer);
            return answer;
        }
    }
}
