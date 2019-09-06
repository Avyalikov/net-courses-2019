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

    class AddClientStrategy : IChoiceStrategy
    {


        public bool CanExecute(string userChoice)
        {
            return userChoice.Equals("1");
        }

        public string Run(RequestSender requestSender, ILoggerService log)
        {
            string welcome = "  Clients registration service.";
            log.Info(welcome);
            Console.WriteLine(welcome); // signal about enter into case  

            string lastName = string.Empty,
                firstName = string.Empty,
                phoneNumber = string.Empty;
            decimal moneyAmount = 0;

            string inputString = string.Empty;
            while (inputString != "e")
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.Write("   Enter the Last name of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Last Name input: {inputString}");
                    if (!StockExchangeValidation.checkClientLastName(inputString)) continue;
                    lastName = inputString;
                }

                if (string.IsNullOrEmpty(firstName))
                {
                    Console.Write("   Enter the First name of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"First Name input: {inputString}");
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

                if (moneyAmount == 0)
                {
                    Console.Write("   Enter the money amount of client: ");
                    inputString = Console.ReadLine();
                    log.Info($"Money amount input: {inputString}");
                    decimal inputDecimal;
                    decimal.TryParse(inputString, out inputDecimal);
                    if (!StockExchangeValidation.checkClientBalanceAmount(inputDecimal)) continue;
                    moneyAmount = inputDecimal;
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
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber,
                Amount = moneyAmount
            };

            log.Info($"Created ClientInputData with LastName = {lastName}, FirstName = { firstName}, PhoneNumber = { phoneNumber}, Amount = { moneyAmount}");

            var reqResult = requestSender.AddClient(clientInputData);
            log.Info($"Request result: {reqResult}.");
            if (string.IsNullOrWhiteSpace(reqResult))
            {
                return "New client was added! Press Enter.";
            }
                        
            return "Error. Client wasn't added! Press Enter.";
        }
    }
}
