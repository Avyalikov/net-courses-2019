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

    class EditClientStrategy : IChoiceStrategy
    {
        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("5");
        }

        public string Run(RequestSender requestSender, ILoggerService log)
        {
            string welcome = "  Edit Clients info service.";
            log.Info(welcome);
            Console.WriteLine(welcome); // signal about enter into case

            int id = 0;
            string lastName = string.Empty,
                firstName = string.Empty,
                phoneNumber = string.Empty;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (id == 0)
                {
                    Console.Write("   Enter the Id of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Id of client input: {inputString}");
                    int inputInt;
                    int.TryParse(inputString, out inputInt);
                    if (!StockExchangeValidation.checkId(inputInt)) continue;
                    id = inputInt;
                }

                if (string.IsNullOrEmpty(lastName))
                {
                    Console.Write("   Enter the Last name of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Last name input: {inputString}");
                    if (!StockExchangeValidation.checkClientLastName(inputString)) continue;
                    lastName = inputString;
                }

                if (string.IsNullOrEmpty(firstName))
                {
                    Console.Write("   Enter the First name of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"First name input: {inputString}");
                    if (!StockExchangeValidation.checkClientFirstName(inputString)) continue;
                    firstName = inputString;
                }

                if (string.IsNullOrEmpty(phoneNumber))
                {
                    Console.Write("   Enter the phone number of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Phone number input: {inputString}");
                    if (!StockExchangeValidation.checkClientPhoneNumber(inputString)) continue;
                    phoneNumber = inputString;
                }

                break;
            }

            if (inputString == "e")
            {
                string exitString = "Exit from registration";
                log.Info(exitString);
                return exitString;
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var clientInputData = new ClientInputData
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber
            };

            log.Info($"Created ClientInputData with Id = {id}, LastName = {lastName}, FirstName = { firstName}, PhoneNumber = { phoneNumber}");

            var reqResult = requestSender.EditClient(clientInputData);
            log.Info($"Request result: {reqResult}.");
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return $"     Client with Id = {id} was changed! Press Enter.";
            }
            return "Error. Client wasn't edited! Press Enter.";
        }
    }
}
