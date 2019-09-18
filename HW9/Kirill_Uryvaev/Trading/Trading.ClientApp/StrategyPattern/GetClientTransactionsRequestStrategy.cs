using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ClientApp.StrategyPattern
{
    class GetClientTransactionsRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("getclienttransactions");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            int id;
            if (splittedUserInpit.Length < 3)
            {
                return "Not enough parameters";
            }
            if (!int.TryParse(splittedUserInpit[1], out id))
            {
                return "Cannot parse ID";
            }

            int top;
            if (!int.TryParse(splittedUserInpit[1], out top))
            {
                return "Cannot parse top";
            }
            requestSender.GetClientsTransactions(id,top, out answer);
            return answer;
        }
    }
}
