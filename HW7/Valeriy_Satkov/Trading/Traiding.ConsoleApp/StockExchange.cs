namespace Traiding.ConsoleApp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using StructureMap;
    using Traiding.ConsoleApp.Logger;
    using System.Net.Http;
    using System.Collections.Generic;
    using Traiding.ConsoleApp.Models;
    using Newtonsoft.Json;
    using System.Text;
    using Traiding.ConsoleApp.Dto;
    using Traiding.ConsoleApp.DependencyInjection;
    using System.Linq;
    using Traiding.ConsoleApp.Strategies;

    public class StockExchange
    {
        private readonly Container traidingRegistryContainer;
        private readonly RequestSender requestSender;
        private readonly ILoggerService log;

        public StockExchange(Container traidingRegistryContainer)
        {
            this.traidingRegistryContainer = traidingRegistryContainer;
            this.requestSender = traidingRegistryContainer.GetInstance<RequestSender>();
            this.log = new Log4NetService(false);
            this.log.InitLogger();
        }        

        public void Start()
        {
            //Console.WriteLine($"{DateTime.Now} Client started");
            Console.WriteLine("Please wait while the database is loading...");

            CancellationTokenSource traidingCancelTokenSource = new CancellationTokenSource();
            CancellationToken traidingCancellationToken = traidingCancelTokenSource.Token;
            TraidingSimulator traidingSimulator = traidingRegistryContainer.GetInstance<TraidingSimulator>();

            Task traidingLive = new Task(() => traidingSimulator.randomDeal(traidingCancellationToken, 10));

            traidingLive.Start();

            log.Info("Traiding sim was started");

            IEnumerable<IChoiceStrategy> choiceStrategies = InitializeRequestStrategy();

            string inputString;
            do
            {
                Console.Clear();
                log.Info("Menu view");

                Console.WriteLine("---The Traiding App---");
                Console.WriteLine("Traiding is running.");
                Console.WriteLine(String.Empty);
                Console.WriteLine("Menu");
                Console.WriteLine(" 1. Add a new client");
                Console.WriteLine(" 2. Edit client info by Id");
                Console.WriteLine(" 3. Remove client by Id");
                Console.WriteLine(" 4. Add a new share");
                Console.WriteLine(" 5. Edit share info by Id");
                Console.WriteLine(" 6. Remove share by Id");
                Console.WriteLine(" 7. Print client balance zone by Client Id");
                Console.WriteLine(" 8. Print client operations");
                Console.WriteLine(String.Empty);
                Console.Write("Type the number or 'e' for exit and press Enter: ");

                inputString = Console.ReadLine();
                log.Info($"User input: {inputString}");
                if (inputString.ToLowerInvariant().Equals("e")) break;

                var choiceStrategy = choiceStrategies.FirstOrDefault(
                    s => s.CanExecute(inputString));

                if (choiceStrategy == null)
                {
                    string answer = "Unknown command";
                    log.Info(answer);
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.WriteLine(choiceStrategy.Run(requestSender, log));
                }

                Console.ReadKey(); // pause
            } while (!inputString.ToLowerInvariant().Equals("e"));

            Console.WriteLine("Good bye");
            Console.ReadKey(); // pause

            traidingCancelTokenSource.Cancel();
            log.Info("Token was cancelled");
            traidingLive.Wait();
            log.Info("Trading was end");
        }

        static IEnumerable<IChoiceStrategy> InitializeRequestStrategy()
        {
            IEnumerable<IChoiceStrategy> choiceStrategies = new List<IChoiceStrategy>()
            {
                new AddClientStrategy(),
                new EditClientStrategy(),
                new DelClientStrategy(),
                new AddShareStrategy(),
                new EditShareStrategy(),
                new DelShareStrategy(),
                new PrintBalanceZoneByClientIdStrategy(),
                new PrintOperationsByClientIdStrategy()
            };
            return choiceStrategies;
        }     
    }
}
