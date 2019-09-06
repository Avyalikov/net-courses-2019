﻿namespace Traiding.ConsoleApp.MenuStrategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;

    class DelShareStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("6");
        }

        public string Run(RequestSender requestSender)
        {
            Console.WriteLine("  Remove Shares service."); // signal about enter into case

            int shareId = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (shareId == 0)
                {
                    Console.Write("   Enter the Id of share for del: ");
                    inputString = Console.ReadLine();
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    shareId = inputInt;
                }

                break;
            }

            if (inputString == "e")
            {
                return "Exit from Remove Shares service";
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var reqResult = requestSender.RemoveShare(shareId);
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return $"     Share with Id = {shareId} was removed! Press Enter.";
            }
            return "Error. Share wasn't removed! Press Enter.";
        }
    }
}
