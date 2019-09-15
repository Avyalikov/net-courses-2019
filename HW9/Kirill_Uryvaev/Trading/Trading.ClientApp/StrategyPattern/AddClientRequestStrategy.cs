using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.DataTransferObjects;

namespace Trading.ClientApp.StrategyPattern
{
    class AddClientRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("addclient");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            if (splittedUserInpit.Length < 3)
            {
                return "Not enough parameters";
            }
            ClientRegistrationInfo clientInfo = new ClientRegistrationInfo
            {
                FirstName = splittedUserInpit[1],
                LastName = splittedUserInpit[2],
                PhoneNumber = splittedUserInpit[3]
            };
            requestSender.PostAddClient(clientInfo, out answer);
            return answer;
        }
    }
}
