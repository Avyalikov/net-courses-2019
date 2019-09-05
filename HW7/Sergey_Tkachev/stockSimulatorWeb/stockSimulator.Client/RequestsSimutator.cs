using stockSimulator.Core.DTO;
using stockSimulator.Core.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using stockSimulator.Core.Models;

namespace stockSimulator.Client
{
    internal class RequestsSimutator
    {
        const int numberOfFunctions = 10;
        const int firstFunction = 1;
        const int exitCode = -1;

        ApiRequests clientRequests = new ApiRequests();

        public RequestsSimutator()
        {
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
                    case 3: UpdateExistingClient(); break;
                    case 4: RemoveExistingClient(); break;
                    case 5: ShowClientStocks(); break;
                    default:
                        break;
                }
            } while (userChoise != exitCode);
        }

        private void ShowClientStocks()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to show his stocks: ");
            int clientId = GetNum();
            string unparsedJson = clientRequests.GetListOfStocksOfClient(clientId);
            List<StockOfClientsEntity> stocks = JsonConvert.DeserializeObject<List<StockOfClientsEntity>>(unparsedJson);
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
                    StockName = stock.Stock.Name,
                    StockType = stock.Stock.Type,
                    StockAmount = stock.Amount,
                    Cost = stock.Stock.Cost
                };
                Console.WriteLine(stocksOfClientInfo);
            }
            Console.WriteLine();
        }

        private void RemoveExistingClient()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to remove: ");
            int clientId = int.Parse(Console.ReadLine());
            string result = clientRequests.RemoveClient(clientId);
            Console.WriteLine("Server answered: " + result);
        }

        private void UpdateExistingClient()
        {
            Console.WriteLine();
            Console.Write("Enter id of client to change: ");
            int id = int.Parse( Console.ReadLine());
            Console.Write("Enter name for this client: ");
            string name = Console.ReadLine();
            Console.Write("Enter surname for this client: ");
            string surname = Console.ReadLine();
            Console.Write("Enter phone number for this client: ");
            string phonenumber = Console.ReadLine();
            Console.Write("Enter account balance for this client: ");
            decimal accountbalance = GetNum();

            UpdateClientInfo changedClient = new UpdateClientInfo
            {
                ID = id,
                Name = name,
                Surname = surname,
                PhoneNumber = phonenumber,
                AccountBalance = accountbalance
            };
            string result = clientRequests.UpdateClient(changedClient);
            Console.WriteLine("Server answered: " + result);
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
            string result = clientRequests.AddNewClient(newClient);
            Console.WriteLine("ID of registered client is " + result);
        }

        private void ShowListOfClients()
        {
            Console.WriteLine();
            Console.Write("Select number of clients on one page: ");
            int numberOfClientsToPrint = GetNum();
            Console.Write("Select number of page to show clients: ");
            int numberOfPages = GetNum();
            string unparsedJson = clientRequests.GetListOfClients(numberOfClientsToPrint, numberOfPages);
            List<ClientEntity> clients = JsonConvert.DeserializeObject<List<ClientEntity>>(unparsedJson);
            if(clients.Count == 0)
            {
                Console.WriteLine("These clients don't exist, please try smaller number.");
                return;
            }
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
            Console.WriteLine();
        }

        private void ShowMenu()
        {
            Console.WriteLine(@"This application provides next functions:
1 - Show list of first 'n' clients.
2 - Add new client.
3 - Update client.
4 - Remove client.
5 - Show client's stocks.
6 - Add new stock to client.
7 - Update client's stock.
8 - Remove client's stock.
9 - Show list of 'n' client's transactions.
10 - Make a new deal between clients.");
        }

        internal void stop()
        {
            throw new NotImplementedException();
        }

        public static int GetNum(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int enteredNum) && ((enteredNum >=min && enteredNum <= max) || enteredNum == exitCode))
                    Console.Write("Incorrect input. Please try again: ");
                else return enteredNum;
        }
    }
}