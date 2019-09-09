using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ClientApp.StrategyPattern
{
    class GetTopTenRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("top10clients");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            int top;
            if (!int.TryParse(splittedUserInpit[1], out top))
            {
                return "Cannot parse ID";
            }
            int page = 1;
            if (splittedUserInpit.Length > 2)
            {
                if (!int.TryParse(splittedUserInpit[2], out page))
                {
                    page = 1;
                }
            }
            requestSender.GetTop10Clients(page, top, out answer);
            return answer;
        }
    }
}
