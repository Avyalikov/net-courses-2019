using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;

namespace Trading.ClientApp.StrategyPattern
{
    class UpdateClientRequestStrategy : IRequestStrategy
    {
        public bool CanExicute(string command)
        {
            return command.ToLower().Equals("updateclient");
        }

        public string Run(string[] splittedUserInpit, RequestSender requestSender)
        {
            string answer = "";
            int clientId;
            if (splittedUserInpit.Length < 5)
            {
                return "Not enough parameters";
            }
            if (!int.TryParse(splittedUserInpit[1], out clientId))
            {
                return "Cannot parse ID";
            }

            var client = new ClientEntity()
            {
                ClientID = clientId,
                ClientFirstName = splittedUserInpit[2],
                ClientLastName = splittedUserInpit[3],
                PhoneNumber = splittedUserInpit[4]
            };

            requestSender.PostUpdateClient(client, out answer);
            return answer;
        }
    }
}
