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
    using Traiding.ConsoleApp.MenuStrategies;
    using System.Linq;

    public class StockExchange
    {
        private readonly Container traidingRegistryContainer;
        private readonly RequestSender requestSender;

        public HttpClient client = new HttpClient();

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

            //CancellationTokenSource traidingCancelTokenSource = new CancellationTokenSource();
            //CancellationToken traidingCancellationToken = traidingCancelTokenSource.Token;

            //Task traidingLive = new Task(() => Traiding(traidingCancellationToken));

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

                Console.ReadKey();

                //switch (inputString)
                //{
                //    case "1":
                //        Console.WriteLine("  Clients registration service."); // signal about enter into case
                //        AddOrEditClient(true);
                //        break;
                //    case "2":
                //        Console.WriteLine("  Edit Clients info service."); // signal about enter into case
                //        AddOrEditClient(false);
                //        break;
                //    case "3":
                //        Console.WriteLine("  Remove Clients service."); // signal about enter into case
                //        DelClient();
                //        break;
                //    case "4":
                //        Console.WriteLine("  Reports service - balances."); // signal about enter into case
                //        PrintBalanceZoneByClientId();
                //        break;
                //    case "5":
                //        Console.WriteLine("  Reports service - operations."); // signal about enter into case
                //        PrintOperationsOfClient();
                //        break;
                //    case "6":
                //        Console.WriteLine("  Shares registration service."); // signal about enter into case
                //        AddOrEditShare(true);
                //        break;
                //    case "7":
                //        Console.WriteLine("  Edit Share info service."); // signal about enter into case
                //        AddOrEditShare(false);
                //        break;
                //    case "8":
                //        Console.WriteLine("  Remove Shares service."); // signal about enter into case
                //        DelShare();
                //        break;
                //    default:
                //        break;
                //}
            } while (!inputString.ToLowerInvariant().Equals("e"));

            Console.WriteLine("Good bye");
            Console.ReadKey();
            //traidingCancelTokenSource.Cancel();
            //traidingLive.Wait();
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

        public void Traiding(CancellationToken token)
        {
            int count = 3;
            int clientsCount;
            int randCustomerId;
            int randSellerId;
            int shareId;
            int sharesNumber;
            List<SharesNumberEntity> sellerSharesNumberList;
            Random rand = new Random();

            while (!token.IsCancellationRequested)
            {
                clientsCount = GetClientsReq(10, 1).Count;
                //sharesCount = this.reportsService.GetSharesCount();
                randCustomerId = rand.Next(1, clientsCount);
                randSellerId = 0;
                sellerSharesNumberList = new List<SharesNumberEntity>();
                while (randSellerId == 0 
                    || randSellerId == randCustomerId 
                    || sellerSharesNumberList.Count == 0)
                {
                    randSellerId = rand.Next(1, clientsCount);
                    sellerSharesNumberList = GetSharesNumbersOfClientReq(randSellerId);
                }

                shareId = sellerSharesNumberList[0].Share.Id;
                int sellerSharesNumber = sellerSharesNumberList[0].Number;
                sharesNumber = 1;
                if (sellerSharesNumber > 1)
                {
                    sharesNumber++;
                }

                if (count == 0)
                {
                    //Console.WriteLine("Now!");
                    DealReq(randCustomerId, randSellerId, shareId, sharesNumber);
                    count = 3;
                }
                else
                {
                    //Console.WriteLine(count);
                    count--;
                }

                Thread.Sleep(400);
            }
        }        

        public List<ClientEntity> GetClientsReq(int number, int page)
        {
            HttpResponseMessage resp = this.client.GetAsync($"clients?top={number}&page={page}").Result;
            resp.EnsureSuccessStatusCode();

            List<ClientEntity> clients = new List<ClientEntity>();
            clients.AddRange(resp.Content.ReadAsAsync<IEnumerable<ClientEntity>>().Result);
            return clients;
        }

        public List<SharesNumberEntity> GetSharesNumbersOfClientReq(int clientId)
        {
            HttpResponseMessage resp = this.client.GetAsync($"shares?clientId={clientId}").Result;
            resp.EnsureSuccessStatusCode();

            List<SharesNumberEntity> sharesNumbers = new List<SharesNumberEntity>();
            sharesNumbers.AddRange(resp.Content.ReadAsAsync<IEnumerable<SharesNumberEntity>>().Result);
            return sharesNumbers;
        }

        public void DealReq(int customerId, int sellerId, int shareId, int sharesNumber)
        {
            var clientInputData = new OperationInputData();
            clientInputData.CustomerId = customerId;
            clientInputData.SellerId = sellerId;
            clientInputData.ShareId = shareId;
            clientInputData.RequiredSharesNumber = sharesNumber;

            var jsonString = JsonConvert.SerializeObject(clientInputData);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = this.client.PostAsync("deal/make", content).Result;
            resp.EnsureSuccessStatusCode();
        }

        //public void AddOrEditClient(bool itIsNewClient)
        //{
        //    int id = 0;
        //    string lastName = string.Empty,
        //        firstName = string.Empty,
        //        phoneNumber = string.Empty;
        //    decimal moneyAmount = 0;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (id == 0 && !itIsNewClient)
        //        {
        //            Console.Write("   Enter the Id of client: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            id = inputInt;
        //        }

        //        if (string.IsNullOrEmpty(lastName))
        //        {
        //            Console.Write("   Enter the Last name of client: ");
        //            inputString = Console.ReadLine();
        //            if (!StockExchangeValidation.checkClientLastName(inputString)) continue;
        //            lastName = inputString;
        //        }

        //        if (string.IsNullOrEmpty(firstName))
        //        {
        //            Console.Write("   Enter the First name of client: ");
        //            inputString = Console.ReadLine();
        //            if (!StockExchangeValidation.checkClientFirstName(inputString)) continue;
        //            firstName = inputString;
        //        }

        //        if (string.IsNullOrEmpty(phoneNumber))
        //        {
        //            Console.Write("   Enter the phone number of client: ");
        //            inputString = Console.ReadLine();
        //            if (!StockExchangeValidation.checkClientPhoneNumber(inputString)) continue;
        //            phoneNumber = inputString;
        //        }

        //        if (moneyAmount == 0 && itIsNewClient)
        //        {
        //            Console.Write("   Enter the money amount of client: ");
        //            inputString = Console.ReadLine();
        //            decimal inputDecimal;
        //            decimal.TryParse(inputString, out inputDecimal);
        //            if (!StockExchangeValidation.checkClientBalanceAmount(inputDecimal)) continue;
        //            moneyAmount = inputDecimal;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }

        //    Console.WriteLine("    Wait a few seconds, please.");

        //    var clientInputData = new ClientInputData();
        //    string reqString = "clients/add";
        //    string goodbyeString = "     New client was added! Press Enter.";

        //    if (itIsNewClient)
        //    {
        //        clientInputData.LastName = lastName;
        //        clientInputData.FirstName = firstName;
        //        clientInputData.PhoneNumber = phoneNumber;
        //        clientInputData.Amount = moneyAmount;
        //    }
        //    else
        //    {
        //        clientInputData.Id = id;
        //        clientInputData.LastName = lastName;
        //        clientInputData.FirstName = firstName;
        //        clientInputData.PhoneNumber = phoneNumber;

        //        reqString = "clients/update";
        //        goodbyeString = $"     Client with Id = {id} was changed! Press Enter.";
        //    }

        //    var jsonString = JsonConvert.SerializeObject(clientInputData);
        //    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    HttpResponseMessage resp = this.client.PostAsync(reqString, content).Result;
        //    resp.EnsureSuccessStatusCode();

        //    Console.WriteLine(goodbyeString);
        //    Console.ReadLine(); // pause
        //}

        //public void DelClient()
        //{
        //    int clientId = 0;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (clientId == 0)
        //        {
        //            Console.Write("   Enter the Id of client for del: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            clientId = inputInt;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }

        //    HttpContent content = new StringContent(inputString, Encoding.UTF8, "application/json");

        //    HttpResponseMessage resp = this.client.PostAsync("clients/remove", content).Result;
        //    resp.EnsureSuccessStatusCode();
        //}

        //public void AddOrEditShare(bool itIsNewShare)
        //{
        //    int id = 0, shareTypeId = 0;
        //    string companyName = string.Empty;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (id == 0 && !itIsNewShare)
        //        {
        //            Console.Write("   Enter the Id of share: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            id = inputInt;
        //        }

        //        if (string.IsNullOrEmpty(companyName))
        //        {
        //            Console.Write("   Enter the Company name: ");
        //            inputString = Console.ReadLine();
        //            if (!StockExchangeValidation.checkCompanyName(inputString)) continue;
        //            companyName = inputString;
        //        }

        //        if (shareTypeId == 0 && !itIsNewShare)
        //        {
        //            Console.Write("   Enter the Id of share type: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            shareTypeId = inputInt;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }

        //    Console.WriteLine("    Wait a few seconds, please.");

        //    var shareInputData = new ShareInputData();
        //    string reqString = "share/add";
        //    string goodbyeString = "     New share was added! Press Enter.";

        //    if (itIsNewShare)
        //    {
        //        shareInputData.CompanyName = companyName;
        //        shareInputData.ShareTypeId = shareTypeId;
        //    }
        //    else
        //    {
        //        shareInputData.Id = id;
        //        shareInputData.CompanyName = companyName;
        //        shareInputData.ShareTypeId = shareTypeId;

        //        reqString = "share/update";
        //        goodbyeString = $"     Share with Id = {id} was changed! Press Enter.";
        //    }

        //    var jsonString = JsonConvert.SerializeObject(shareInputData);
        //    HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    HttpResponseMessage resp = this.client.PostAsync(reqString, content).Result;
        //    resp.EnsureSuccessStatusCode();

        //    Console.WriteLine(goodbyeString);
        //    Console.ReadLine(); // pause
        //}

        //public void DelShare()
        //{
        //    int shareId = 0;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (shareId == 0)
        //        {
        //            Console.Write("   Enter the Id of share for del: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            shareId = inputInt;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }

        //    HttpContent content = new StringContent(inputString, Encoding.UTF8, "application/json");

        //    HttpResponseMessage resp = this.client.PostAsync("share/remove", content).Result;
        //    resp.EnsureSuccessStatusCode();
        //}

        //public void PrintBalanceZoneByClientId()
        //{
        //    int clientId = 0;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (clientId == 0)
        //        {
        //            Console.Write("   Enter the Id of client: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            clientId = inputInt;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }

        //    HttpResponseMessage resp = this.client.GetAsync($"balances?clientId={clientId}").Result;
        //    resp.EnsureSuccessStatusCode();
            
        //    var color = resp.Content.ReadAsAsync<string>().Result;
        //    Console.WriteLine($"Balance zone for client Id = {clientId} — {color}.");
        //    Console.ReadKey();
        //}

        //public void PrintOperationsOfClient()
        //{
        //    int clientId = 0;
        //    int top = 0;

        //    string inputString = string.Empty;
        //    while (inputString != "e")
        //    {
        //        if (clientId == 0)
        //        {
        //            Console.Write("   Enter the Id of client: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            clientId = inputInt;
        //        }

        //        if (top == 0)
        //        {
        //            Console.Write("   Enter the number of operations for view: ");
        //            inputString = Console.ReadLine();
        //            int inputInt;
        //            int.TryParse(inputString, out inputInt);
        //            if (!StockExchangeValidation.checkId(inputInt)) continue;
        //            clientId = inputInt;
        //        }

        //        break;
        //    }

        //    if (inputString == "e")
        //    {
        //        return;
        //    }


        //    HttpResponseMessage resp = this.client.GetAsync($"transactions?clientId={clientId}&top={top}").Result;
        //    resp.EnsureSuccessStatusCode();

        //    var operations = resp.Content.ReadAsAsync<IEnumerable<OperationEntity>>().Result;

        //    foreach (var operation in operations)
        //    {
        //        Console.WriteLine(
        //            $"Id: {operation.Id}" +
        //            $"Debit Date: {operation.DebitDate}" +
        //            $"Customer Id: {operation.Customer.Id}" +
        //            $"Charge Date: {operation.ChargeDate}" +
        //            $"Seller Id: {operation.Seller.Id}" +
        //            $"Share Id: {operation.Share.Id}" +
        //            $"Share type name: {operation.ShareTypeName}" +
        //            $"Cost: {operation.Cost}" +
        //            $"Number: {operation.Number}" +
        //            $"Total: {operation.Total}");
        //    }
        //}        
    }
}
