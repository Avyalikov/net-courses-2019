namespace Traiding.ConsoleApp.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;

    class DelClientStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("3");
        }

        public string Run(RequestSender requestSender)
        {
            Console.WriteLine("  Remove Clients service."); // signal about enter into case

            int clientId = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (clientId == 0)
                {
                    Console.Write("   Enter the Id of client for del: ");
                    inputString = Console.ReadLine();
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    clientId = inputInt;
                }

                break;
            }

            if (inputString == "e")
            {
                return "Exit from Remove Clients service";
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var reqResult = requestSender.RemoveClient(clientId);
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return $"     Client with Id = {clientId} was removed! Press Enter.";
            }
            return "Error. Client wasn't removed! Press Enter.";
        }
    }
}
