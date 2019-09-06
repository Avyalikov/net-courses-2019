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

        // public HttpClient client = new HttpClient();

        public StockExchange(Container traidingRegistryContainer, string APIaddress)
        {
            this.traidingRegistryContainer = traidingRegistryContainer;
            this.requestSender = traidingRegistryContainer.GetInstance<RequestSender>();
            this.requestSender.SetBaseAddress(APIaddress);

            //client.BaseAddress = new Uri("http://localhost:52804");
        }        

        public void Start()
        {
            //Console.WriteLine($"{DateTime.Now} Client started");
            Console.WriteLine("Please wait while the database is loading...");

            //GetClientsReq(1, 1); // first call of db

            CancellationTokenSource traidingCancelTokenSource = new CancellationTokenSource();
            CancellationToken traidingCancellationToken = traidingCancelTokenSource.Token;
            TraidingSimulator traidingSimulator = new TraidingSimulator(requestSender);

            Task traidingLive = new Task(() => traidingSimulator.Start(traidingCancellationToken));

            //traidingLive.Start();

            IEnumerable<IChoiceStrategy> choiceStrategies = InitializeRequestStrategy();

            string inputString;
            do
            {
                Console.Clear();

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
                if (inputString.ToLowerInvariant().Equals("e")) break;

                var choiceStrategy = choiceStrategies.FirstOrDefault(
                    s => s.CanExecute(inputString));

                if (choiceStrategy == null)
                {
                    Console.WriteLine("Unknown command");
                }
                else
                {
                    Console.WriteLine(choiceStrategy.Run(requestSender));
                }

                Console.ReadKey(); // pause
            } while (!inputString.ToLowerInvariant().Equals("e"));

            Console.WriteLine("Good bye");
            Console.ReadKey();

            traidingCancelTokenSource.Cancel();
            traidingLive.Wait();
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

        //public void Traiding(CancellationToken token)
        //{
        //    int count = 3;
        //    int clientsCount;
        //    int randCustomerId;
        //    int randSellerId;
        //    int shareId;
        //    int sharesNumber;
        //    List<SharesNumberEntity> sellerSharesNumberList;
        //    Random rand = new Random();

        //    while (!token.IsCancellationRequested)
        //    {
        //        clientsCount = GetClientsReq(10, 1).Count;
        //        //sharesCount = this.reportsService.GetSharesCount();
        //        randCustomerId = rand.Next(1, clientsCount);
        //        randSellerId = 0;
        //        sellerSharesNumberList = new List<SharesNumberEntity>();
        //        while (randSellerId == 0 
        //            || randSellerId == randCustomerId 
        //            || sellerSharesNumberList.Count == 0)
        //        {
        //            randSellerId = rand.Next(1, clientsCount);
        //            sellerSharesNumberList = GetSharesNumbersOfClientReq(randSellerId);
        //        }

        //        shareId = sellerSharesNumberList[0].Share.Id;
        //        int sellerSharesNumber = sellerSharesNumberList[0].Number;
        //        sharesNumber = 1;
        //        if (sellerSharesNumber > 1)
        //        {
        //            sharesNumber++;
        //        }

        //        if (count == 0)
        //        {
        //            //Console.WriteLine("Now!");
        //            DealReq(randCustomerId, randSellerId, shareId, sharesNumber);
        //            count = 3;
        //        }
        //        else
        //        {
        //            //Console.WriteLine(count);
        //            count--;
        //        }

        //        Thread.Sleep(400);
        //    }
        //}        

        //public List<ClientEntity> GetClientsReq(int number, int page)
        //{
        //    HttpResponseMessage resp = this.client.GetAsync($"clients?top={number}&page={page}").Result;
        //    resp.EnsureSuccessStatusCode();

        //    List<ClientEntity> clients = new List<ClientEntity>();
        //    clients.AddRange(resp.Content.ReadAsAsync<IEnumerable<ClientEntity>>().Result);
        //    return clients;
        //}

        //public List<SharesNumberEntity> GetSharesNumbersOfClientReq(int clientId)
        //{
        //    HttpResponseMessage resp = this.client.GetAsync($"shares?clientId={clientId}").Result;
        //    resp.EnsureSuccessStatusCode();

        //    List<SharesNumberEntity> sharesNumbers = new List<SharesNumberEntity>();
        //    sharesNumbers.AddRange(resp.Content.ReadAsAsync<IEnumerable<SharesNumberEntity>>().Result);
        //    return sharesNumbers;
        //}

        //public void DealReq(int customerId, int sellerId, int shareId, int sharesNumber)
        //{
        //    var clientInputData = new OperationInputData
        //    {
        //        CustomerId = customerId,
        //        SellerId = sellerId,
        //        ShareId = shareId,
        //        RequiredSharesNumber = sharesNumber
        //    };

        //    var jsonString = JsonConvert.SerializeObject(clientInputData);
        //    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    HttpResponseMessage resp = this.client.PostAsync("deal/make", content).Result;
        //    resp.EnsureSuccessStatusCode();
        //}      
    }
}
