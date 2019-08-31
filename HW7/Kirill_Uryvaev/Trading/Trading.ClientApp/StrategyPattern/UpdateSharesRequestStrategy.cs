using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.ClientApp.StrategyPattern
{
    class UpdateSharesRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("updateshares");
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

            var share = new ClientsSharesEntity()
            {
                ClientID = clientID,
                ShareID = shareID,
                CostOfOneShare = cost,
                Amount = amount
            };

            requestSender.PostUpdateShare(share, out answer);
            return answer;
        }
    }
}
