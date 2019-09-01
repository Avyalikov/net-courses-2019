using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.ClientApp.StrategyPattern
{
    class MakeDealRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("makedeal");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            if (splittedUserInpit.Length < 3)
            {
                return "Not enough parameters";
            }
            int firstClientId;
            if (!int.TryParse(splittedUserInpit[1], out firstClientId))
            {
                return "Cannot parse ID";
            }
            int secondClientId;
            if (!int.TryParse(splittedUserInpit[2], out secondClientId))
            {
                return "Cannot parse ID";
            }
            int shareID;
            if (!int.TryParse(splittedUserInpit[2], out shareID))
            {
                return "Cannot parse ID";
            }
            int amount;
            if (!int.TryParse(splittedUserInpit[2], out amount))
            {
                return "Cannot parse amount";
            }

            TransactionHistoryInfo transactionInfo = new TransactionHistoryInfo()
            {
                BuyerClientID = firstClientId,
                SellerClientID = secondClientId,
                DateTime = DateTime.Now,
                Amount = amount,
                ShareID = shareID,                
            };

            requestSender.PostMakeDeal(transactionInfo, out answer);
            return answer;
        }
    }
}
