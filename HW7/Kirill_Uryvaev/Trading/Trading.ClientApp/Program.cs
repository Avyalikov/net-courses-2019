using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.Core.DataTransferObjects;

namespace Trading.ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegistry());
            string exitKey = "e";
            string userInput = "";
            RequestSender requestSender = container.GetInstance<RequestSender>();
            Console.WriteLine($"{DateTime.Now} Client started");
            while (!userInput.ToLower().Equals(exitKey))
            {
                userInput = Console.ReadLine();
                Console.WriteLine(GetRequestResult(userInput, requestSender));
            }
            
        }
        static string GetRequestResult(string userInput, RequestSender requestSender)
        {
            string answer ="";
            string[] splittedUserInpit = userInput.Split(' ','\t');
            switch (splittedUserInpit[0].ToLower())
            {
                case "top10clients":
                    int page = 1;
                    if (splittedUserInpit.Length > 1)
                    {
                        if (!int.TryParse(splittedUserInpit[1], out page))
                        {
                            page = 1;
                        }
                    }
                    requestSender.GetTop10Clients(page, out answer);
                    return answer;
                case "addclient":
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
                default:
                    return "Unknown command";
            }            
        }
    }
}
