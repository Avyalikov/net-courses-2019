namespace Traiding.ConsoleApp.MenuStrategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;

    class PrintOperationsByClientIdStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("8");
        }

        public string Run(RequestSender requestSender)
        {
            Console.WriteLine("  Reports service - operations"); // signal about enter into case

            int clientId = 0;
            int top = 0;

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

                if (top == 0)
                {
                    Console.Write("   Enter the number of operations for view: ");
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
                return "Exit from Reports service - operations";
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var reqResult = requestSender.GetClientOperations(clientId, top);
            if (reqResult != null)
            {
                foreach (var operation in reqResult)
                {
                    Console.WriteLine(
                        $"Id: {operation.Id}" +
                        $"Debit Date: {operation.DebitDate}" +
                        $"Customer Id: {operation.Customer.Id}" +
                        $"Charge Date: {operation.ChargeDate}" +
                        $"Seller Id: {operation.Seller.Id}" +
                        $"Share Id: {operation.Share.Id}" +
                        $"Share type name: {operation.ShareTypeName}" +
                        $"Cost: {operation.Cost}" +
                        $"Number: {operation.Number}" +
                        $"Total: {operation.Total}");
                }
                return "List of client operations";
            }
            return "Error. Client wasn't finded! Press Enter.";
        }
    }
}
