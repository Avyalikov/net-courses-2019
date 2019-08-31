using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using Trading.ClientApp.StrategyPattern;
using System.Timers;

namespace Trading.ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingRegistry());
            string exitKey = "e";
            string userInput = "";
            RequestSender requestSender = container.GetInstance<RequestSender>();
            Console.WriteLine($"{DateTime.Now} Client started");
            Timer timer = new Timer(10000) { AutoReset = true };
            timer.Elapsed += (sender, e) => { container.GetInstance<TradeSimulator>().ClientsTrade(); };
            timer.Start();

            IEnumerable<IRequestStrategy> strategies = InitializeRequestStrategy();

            while (!userInput.ToLower().Equals(exitKey))
            {
                userInput = Console.ReadLine();
                Console.WriteLine(GetRequestResult(userInput, requestSender, strategies));
            }

        }
        static string GetRequestResult(string userInput, RequestSender requestSender, IEnumerable<IRequestStrategy> strategies)
        {
            string[] splittedUserInpit = userInput.Split(' ', '\t');
            var strategy = strategies.FirstOrDefault(x => x.CanExicute(splittedUserInpit[0]));
            if (strategy == null)
            {
                return "Unknown command";
            }
            return strategy.Run(splittedUserInpit, requestSender);
        }
        static IEnumerable<IRequestStrategy> InitializeRequestStrategy()
        {
            IEnumerable<IRequestStrategy> requestStrategies = new List<IRequestStrategy>()
            {
                new AddClientRequestStrategy(),
                new AddSharesRequestStrategy(),
                new GetClientsSharesRequestStrategy(), 
                new GetTopTenRequestStrategy(),
                new UpdateClientRequestStrategy(),
                new UpdateSharesRequestStrategy(),
                new RemoveClientRequestStrategy(),
                new RemoveSharesRequestStrategy(),
                new GetClientZoneRequestStrategy(),
                new GetClientTransactionsRequestStrategy(),
                new MakeDealRequestStrategy()
            };
            return requestStrategies;
        }
    }
}
