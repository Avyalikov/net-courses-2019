namespace Traiding.ConsoleApp
{
    using System;
    using System.Threading;
    using StructureMap;
    using Traiding.Core.Dto;
    using Traiding.Core.Services;

    public class StockExchange
    {
        private readonly Container traidingRegistryContainer;
        private readonly BalancesService balancesService;
        private readonly ClientsService clientsService;
        private readonly ReportsService reportsService;
        private readonly SalesService salesService;
        private readonly SharesService sharesService;
        private readonly ShareTypesService shareTypesService;        

        public StockExchange(Container traidingRegistryContainer)
        {
            this.traidingRegistryContainer = traidingRegistryContainer;
            this.balancesService = traidingRegistryContainer.GetInstance<BalancesService>();
            this.clientsService = traidingRegistryContainer.GetInstance<ClientsService>();            
            this.reportsService = traidingRegistryContainer.GetInstance<ReportsService>();
            this.salesService = traidingRegistryContainer.GetInstance<SalesService>();
            this.sharesService = traidingRegistryContainer.GetInstance<SharesService>();
            this.shareTypesService = traidingRegistryContainer.GetInstance<ShareTypesService>();            
        }

        public void traiding()
        {
            int count = 3;
            int clientsCount;
            int sharesCount;
            int randClientId;
            int randShareId;
            int randSharesNumber;
            Random rand = new Random();

            while (true)
            {
                clientsCount = this.reportsService.GetClientsCount();
                sharesCount = this.reportsService.GetSharesCount();
                randClientId = rand.Next(1, clientsCount);
                randShareId = rand.Next(1, sharesCount);
                randSharesNumber = rand.Next(1, 2);
                if (count == 0)
                {
                    //Console.WriteLine("Now!");
                    this.salesService.Deal(randClientId, randShareId, randSharesNumber);
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

        public void Start()
        {
            Console.WriteLine("Please wait while the database is loading...");
            salesService.RemoveOperation(salesService.CreateOperation());

            /* ---The Traiding App---
             * 
             * Traiding Live!
             * Time until the next deal: ...[10] // countdown to next deal from 10 to 1, Now
             * Last deal: [] // last operation from dto.Operations
             * 
             * Enter 'm' for switch to Menu or 'e' for Exit.
             */

            /* ---The Traiding App---
             * Traiding is running.
             * 
             * Menu
             *  1. Add a new client
             *  2. Clients in 'Orange' zone // client with zero balances
             *  3. Add a new share into system
             *  4. Add a new share type into system
             *  5. Change the cost of share type
             *  6. View Deal History
             *  7. Change share type for share
             * 
             * Enter the number for action or 't' for switch to 'Traiding Live!' or 'e' for Exit.
             */

            Thread traidingLive = new Thread(traiding);

            traidingLive.Start();

            string inputString;
            do
            {
                Console.Clear();

                Console.WriteLine("---The Traiding App---");
                Console.WriteLine("Traiding is running.");
                Console.WriteLine(String.Empty);
                Console.WriteLine("Menu");
                Console.WriteLine(" 1. Add a new client");
                Console.WriteLine(" 2. Clients in 'Orange' zone");
                //Console.WriteLine(" 6. View Deal History");
                Console.WriteLine(String.Empty);
                Console.Write("Type the number or 'e' for exit and press Enter: ");

                inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        Console.WriteLine("  Client registration service."); // signal about enter into case
                        AddClient();
                        // ioProvider.ReadLine(); // pause
                        break;
                    case "2":
                        Console.WriteLine("  Clients in 'Orange' zone — clients with zero balances"); // signal about enter into case
                        viewClientsWithZeroBalances();
                        break;
                    //case "6":
                    //    Console.WriteLine("  Deal history (Last 10)."); // signal about enter into case
                    //    viewDealHistory();
                    //    break;
                    default:
                        break;
                }
            } while (inputString != "e");

            //Console.ReadKey();
            traidingLive.Abort();
        }

        //public void viewDealHistory()
        //{
        //    var operations = reportsService.GetTop10Operations();
        //    Console.WriteLine("   Id  DebitDate  Customer   ChargeDate Seller Share ShareTypeName Cost Number Total");
        //    foreach (var item in operations)
        //    {
        //        Console.WriteLine($"   { item.Id } {item.DebitDate} {item.Customer} {item.ChargeDate} {item.Seller} {item.Share} {item.ShareTypeName} {item.Cost} {item.Number} {item.Total}");
        //    }
        //    Console.WriteLine("    Last 10 Deals. Press Enter.");
        //    Console.ReadLine(); // pause
        //}

        public void viewClientsWithZeroBalances()
        {
            var clients = reportsService.GetZeroBalances();
            Console.WriteLine("   Id  Client");
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
            Console.WriteLine("    Return clients with zero balances. Press Enter.");
            Console.ReadLine(); // pause
        }

        public void AddClient()
        {
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
                    if (!StockExchangeValidation.checkClientLastName(inputString)) continue;                    
                    lastName = inputString;
                }

                if (string.IsNullOrEmpty(firstName))
                {
                    Console.Write("   Enter the First name of client: ");
                    inputString = Console.ReadLine();
                    if (!StockExchangeValidation.checkClientFirstName(inputString)) continue;
                    firstName = inputString;
                }

                if (string.IsNullOrEmpty(phoneNumber))
                {
                    Console.Write("   Enter the phone number of client: ");
                    inputString = Console.ReadLine();
                    if (!StockExchangeValidation.checkClientPhoneNumber(inputString)) continue;
                    phoneNumber = inputString;
                }

                if (moneyAmount == 0)
                {
                    Console.Write("   Enter the money amount of client: ");
                    inputString = Console.ReadLine();
                    decimal inputDecimal;
                    decimal.TryParse(inputString, out inputDecimal);
                    if (!StockExchangeValidation.checkClientBalanceAmount(inputDecimal)) continue;
                    moneyAmount = inputDecimal;
                }

                break;
            }

            if (inputString == "e")
            {
                return;
            }

            Console.WriteLine("    Wait a few seconds, please.");

            var clientId = clientsService.RegisterNewClient(new ClientRegistrationInfo()
            {
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber                
            });
            balancesService.RegisterNewBalance(new BalanceRegistrationInfo()
            {
                Client = clientsService.GetClient(clientId),
                Amount = moneyAmount
            });

            Console.WriteLine("     New client was added! Press Enter.");
            Console.ReadLine(); // pause
        }
    }
}
