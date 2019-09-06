namespace Traiding.ConsoleApp.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;

    class PrintBalanceZoneByClientIdStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("7");
        }

        public string Run(RequestSender requestSender)
        {
            Console.WriteLine("  Reports service - balance zone"); // signal about enter into case

            int clientId = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (clientId == 0)
                {
                    Console.Write("   Enter the Id of client: ");
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
                return "Exit from Reports service - balance zone";
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var reqResult = requestSender.GetBalanceZoneColor(clientId);
            if (!string.IsNullOrWhiteSpace(reqResult))
            {
                return $"Balance zone for client Id = {clientId} — {reqResult}.";
            }
            return "Error. Client wasn't finded! Press Enter.";
        }
    }
}
