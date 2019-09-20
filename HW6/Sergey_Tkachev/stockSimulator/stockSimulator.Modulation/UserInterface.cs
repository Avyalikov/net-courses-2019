using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Services;
using stockSimulator.Modulation.Dependencies;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Modulation
{
    internal class UserInterface
    {
        const int numberOfFunctions = 10;
        const int firstFunction = 1;
        const int exitCode = -1;
        private readonly ClientService clientService;
        private readonly EditCleintStockService editCleintStockService;

        public UserInterface()
        {
            var container = new Container(new StockSimulatorRegistry());
            this.clientService = container.GetInstance<ClientService>();
            this.editCleintStockService = container.GetInstance<EditCleintStockService>();
        }

        internal void start()
        {
            int userChoise;
            do
            {
                ShowMenu();
                Console.Write("Choose one of numbers or print '-1' to exit: ");
                userChoise = GetNum(firstFunction, numberOfFunctions);
                switch (userChoise)
                {
                    case 1: ShowListOfClients(); break;
                    case 2: AddNewClient(); break;
                    case 3: ShowClientStocks(); break;
                    case 4: AddNewStockToClient(); break;
                    case 5: UpdateStockOfClient(); break;
                    case 6: GetClientsWithMoney(); break;
                    case 7: GetClientsWithZeroMoney(); break;
                    case 8: GetClientsWithoutMoney(); break;
                    default:
                        break;
                }

            } while (userChoise != exitCode);
        }

        private void GetClientsWithoutMoney()
        {
            var clients = clientService.GetClientsWithNegativeBalance().ToList();
            if(clients.Count == 0)
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

        private void GetClientsWithZeroMoney()
        {
            var clients = clientService.GetClientsWithZeroBalance().ToList();
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

        private void GetClientsWithMoney()
        {
            var clients = clientService.GetClientsWithPositiveBalance().ToList();
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
            string result = editCleintStockService.Edit(editStockOfClientInfo);
            Console.WriteLine(result);
            Console.WriteLine();
        }

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
            string result = editCleintStockService.addStock(editStockOfClientInfo);
            Console.WriteLine("ID of added entity is: " + result);
            Console.WriteLine();
        }

        private void ShowClientStocks()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to show his stocks: ");
            int clientId = GetNum();

            List<StockOfClientsEntity> stocks = editCleintStockService.GetStocksOfClient(clientId).ToList();
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
            int result = clientService.RegisterNewClient(newClient);
            Console.WriteLine("ID of registered client is " + result);
        }

        private void ShowListOfClients()
        {
            var clients = clientService.GetClients();
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
            Console.WriteLine();
        }

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

        public static int GetNum(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int enteredNum) && ((enteredNum >= min && enteredNum <= max) || enteredNum == exitCode))
                    Console.Write("Incorrect input. Please try again: ");
                else return enteredNum;
        }
    }
}
