namespace Traiding.ConsoleApp.MenuStrategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;

    class EditShareStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("4");
        }

        public string Run(RequestSender requestSender)
        {
            Console.WriteLine("  Edit Share info service"); // signal about enter into case

            int id = 0, shareTypeId = 0;
            string companyName = string.Empty;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (id == 0)
                {
                    Console.Write("   Enter the Id of share: ");
                    inputString = Console.ReadLine();
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    id = inputInt;
                }

                if (string.IsNullOrEmpty(companyName))
                {
                    Console.Write("   Enter the Company name: ");
                    inputString = Console.ReadLine();
                    if (!StockExchangeValidation.checkCompanyName(inputString)) continue;
                    companyName = inputString;
                }

                if (shareTypeId == 0)
                {
                    Console.Write("   Enter the Id of share type: ");
                    inputString = Console.ReadLine();
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    shareTypeId = inputInt;
                }

                break;
            }

            if (inputString == "e")
            {
                return "Exit from Edit Share info service";
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var shareInputData = new ShareInputData
            {
                Id = id,
                CompanyName = companyName,
                ShareTypeId = shareTypeId
            };

            var reqResult = requestSender.EditShare(shareInputData);
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return $"Share with Id = {id} was changed! Press Enter.";
            }
            return "Error. Share wasn't changed! Press Enter.";
        }
    }
}
