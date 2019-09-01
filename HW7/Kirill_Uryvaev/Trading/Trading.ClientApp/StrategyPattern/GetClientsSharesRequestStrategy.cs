using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ClientApp.StrategyPattern
{
    class GetClientsSharesRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("getclientsshares");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            int id;
            if (splittedUserInpit.Length < 2)
            {
                return "Not enough parameters";
            }
            if (!int.TryParse(splittedUserInpit[1], out id))
            {
                return "Cannot parse ID";
            }

            requestSender.GetClientsShares(id, out answer);
            return answer;
        }
    }
}
