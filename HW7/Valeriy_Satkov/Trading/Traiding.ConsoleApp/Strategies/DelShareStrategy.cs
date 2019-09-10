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

    class DelShareStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("6");
        }

        public string Run(RequestSender requestSender, ILoggerService log)
        {
            string welcome = "  Remove Shares service.";
            log.Info(welcome);
            Console.WriteLine(welcome); // signal about enter into case

            int shareId = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (shareId == 0)
                {
                    Console.Write("   Enter the Id of share for del: ");
                    inputString = Console.ReadLine();
                    log.Info($"Id of share for del input: {inputString}");
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    shareId = inputInt;
                }

                break;
            }

            if (inputString == "e")
            {
                string exitString = "Exit from Remove Shares service";
                log.Info(exitString);
                return exitString;
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var reqResult = requestSender.RemoveShare(shareId);
            log.Info($"Request result: {reqResult}.");
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return $"     Share with Id = {shareId} was removed! Press Enter.";
            }
            return "Error. Share wasn't removed! Press Enter.";
        }
    }
}
