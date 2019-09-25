namespace Traiding.ConsoleApp.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;
    using Traiding.ConsoleApp.Logger;

    class PrintOperationsByClientIdStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("8");
        }

        public string Run(RequestSender requestSender, ILoggerService log)
        {
            string welcome = "  Reports service - operations";
            log.Info(welcome);
            Console.WriteLine(welcome); // signal about enter into case

            int clientId = 0;
            int top = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (clientId == 0)
                {
                    Console.Write("   Enter the Id of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Id of client input: {inputString}");
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    clientId = inputInt;
                }

                if (top == 0)
                {
                    Console.Write("   Enter the number of operations for view: ");
                    inputString = Console.ReadLine();
                    log.Info($"Number of operations for view input: {inputString}");
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    top = inputInt;
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
                StringBuilder operationString;
                foreach (var operation in reqResult)
                {
                    operationString = new StringBuilder();
                    operationString.Append($"Id: {operation.Id}\n");
                    operationString.Append($" Debit Date: {operation.DebitDate}\n");
                    operationString.Append($" Customer Id: {operation.Customer.Id}\n");
                    operationString.Append($" Charge Date: {operation.ChargeDate}\n");
                    operationString.Append($" Seller Id: {operation.Seller.Id}\n");
                    operationString.Append($" Share Id: {operation.Share.Id}\n");
                    operationString.Append($" Share Type Name: {operation.ShareTypeName}\n");
                    operationString.Append($" Cost: {operation.Cost}\n");
                    operationString.Append($" Number: {operation.Number}\n");
                    operationString.Append($" Total: {operation.Total}");
                    Console.WriteLine(operationString.ToString());
                    log.Info(operationString.ToString());
                }
                return $"List of operations for clientId = {clientId}";
            }
            log.Info("Error. Client wasn't found!");
            return "Error. Client wasn't found! Press Enter.";
        }
    }
}
