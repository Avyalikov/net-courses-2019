namespace stockSimulator.Modulation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Services;
    using stockSimulator.Modulation.Dependencies;
    using StructureMap;

    internal class UserInterface
    {
        private const int NumberOfFunctions = 10;
        private const int FirstFunction = 1;
        private const int ExitCode = -1;
        private readonly ClientService clientService;
        private readonly EditCleintStockService editCleintStockService;

        /// <summary>
        /// Initializes an instance of UserInterface class.
        /// </summary>
        public UserInterface()
        {
            var container = new Container(new StockSimulatorRegistry());
            this.clientService = container.GetInstance<ClientService>();
            this.editCleintStockService = container.GetInstance<EditCleintStockService>();
        }

        /// <summary>
        /// Starts user interface logic.
        /// </summary>
        internal void Start()
        {
            int userChoise;
            do
            {
                this.ShowMenu();
                Console.Write("Choose one of numbers or print '-1' to exit: ");
                userChoise = GetNum(FirstFunction, NumberOfFunctions);
                switch (userChoise)
                {
                    case 1: this.ShowListOfClients(); break;
                    case 2: this.AddNewClient(); break;
                    case 4: this.AddNewStockToClient(); break;
                    case 5: this.UpdateStockOfClient(); break;
                    case 6: this.GetClientsWithMoney(); break;
                    case 7: this.GetClientsWithZeroMoney(); break;
                    case 8: this.GetClientsWithoutMoney(); break;
                    default:
                        break;
                }
            }
            while (userChoise != ExitCode);
        }

        /// <summary>
        /// Shows clients with negative balance.
        /// </summary>
        private void GetClientsWithoutMoney()
        {
            var clients = this.clientService.GetClientsWithNegativeBalance().ToList();
            if (clients.Count == 0)
            {
                Console.WriteLine("There are no clients with negative balance.");
                Console.WriteLine();
                return;
            }

            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Shows clients with zero balance.
        /// </summary>
        private void GetClientsWithZeroMoney()
        {
            var clients = this.clientService.GetClientsWithZeroBalance().ToList();
            if (clients.Count == 0)
            {
                Console.WriteLine("There are no clients with zero balance.");
                Console.WriteLine();
                return;
            }

            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Shows clients with positive balance.
        /// </summary>
        private void GetClientsWithMoney()
        {
            var clients = this.clientService.GetClientsWithPositiveBalance().ToList();
            if (clients.Count == 0)
            {
                Console.WriteLine("There are no clients with positive balance.");
                Console.WriteLine();
                return;
            }

            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Updates info about client's stock using user's input data.
        /// </summary>
        private void UpdateStockOfClient()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to update information about his stocks: ");
            int clientId = GetNum();
            Console.Write("Enter id of his stock to edit: ");
            int stockId = GetNum();
            Console.Write("Enter amount of entered stock to edit: ");
            int stockAmount = GetNum();
            EditStockOfClientInfo editStockOfClientInfo = new EditStockOfClientInfo
            {
                AmountOfStocks = stockAmount,
                Client_ID = clientId,
                Stock_ID = stockId
            };
            string result = this.editCleintStockService.Edit(editStockOfClientInfo);
            Console.WriteLine(result);
            Console.WriteLine();
        }

        /// <summary>
        /// Adds new stock to client using user's input data.
        /// </summary>
        private void AddNewStockToClient()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to add him new stock: ");
            int clientId = GetNum();
            Console.Write("Enter id of stock to add: ");
            int stockId = GetNum();
            Console.Write("Enter amount of stock to add: ");
            int stockAmount = GetNum();
            EditStockOfClientInfo editStockOfClientInfo = new EditStockOfClientInfo
            {
                AmountOfStocks = stockAmount,
                Client_ID = clientId,
                Stock_ID = stockId
            };
            string result = this.editCleintStockService.AddStock(editStockOfClientInfo);
            Console.WriteLine("ID of added entity is: " + result);
            Console.WriteLine();
        }

        /// <summary>
        /// Shows stocks of clients.
        /// </summary>
        private void ShowClientStocks()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to show his stocks: ");
            int clientId = GetNum();

            List<StockOfClientsEntity> stocks = this.editCleintStockService.GetStocksOfClient(clientId).ToList();
            if (stocks.Count == 0)
            {
                Console.WriteLine("This client doesn't have any stocks.");
                return;
            }

            Console.WriteLine("This client has the next stocks:");
            foreach (var stock in stocks)
            {
                StocksOfClientInfo stocksOfClientInfo = new StocksOfClientInfo
                {
                    StockID = stock.Stock.ID,
                    StockName = stock.Stock.Name,
                    StockType = stock.Stock.Type,
                    StockAmount = stock.Amount,
                    Cost = stock.Stock.Cost
                };

                Console.WriteLine(stocksOfClientInfo);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Adds new client using user's input data.
        /// </summary>
        private void AddNewClient()
        {
            Console.WriteLine();
            Console.Write("Enter name for new client: ");
            string name = Console.ReadLine();
            Console.Write("Enter surname for new client: ");
            string surname = Console.ReadLine();
            Console.Write("Enter phone number for new client: ");
            string phonenumber = Console.ReadLine();
            Console.Write("Enter account balance for new client: ");
            decimal accountbalance = GetNum();

            ClientRegistrationInfo newClient = new ClientRegistrationInfo
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phonenumber,
                AccountBalance = accountbalance
            };
            int result = this.clientService.RegisterNewClient(newClient);
            Console.WriteLine("ID of registered client is " + result);
        }

        /// <summary>
        /// Shows all clients.
        /// </summary>
        private void ShowListOfClients()
        {
            var clients = this.clientService.GetClients();
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Shows a menu with available optionts for user.
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine(@"This application provides next functions:
1 - Show list of clients.
2 - Add new client.
3 - Show client's stocks.
4 - Add new stock to client.
5 - Update client's stock.
6 - Get all clients in Green zone.
7 - Get all clients in Orange zone.
8 - Get all clients in Black zone");
        }

        /// <summary>
        /// Reads a number from console and checks it is between min and max values.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns></returns>
        public static int GetNum(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int enteredNum) && ((enteredNum >= min && enteredNum <= max) || enteredNum == ExitCode))
                {
                    Console.Write("Incorrect input. Please try again: ");
                }
                else
                {
                    return enteredNum;
                }
            }
        }
    }
}
