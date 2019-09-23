namespace TradeSimulator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Dto;
    using TradeSimulator.Core.Models;
    using TradeSimulator.Core.Repositories;

    public class ClientsService
    {
        private readonly IClientsTableRepository clientsTableRepository;
        private readonly IAccountTableRepository accountTableRepository;
        private readonly IStockPriceTableRepository stockPriceTableRepository;
        private readonly IStockOfClientTableRepository stockOfClientTableRepository;
        private readonly ILogger logger;

        public ClientsService(
            IClientsTableRepository clientsTableRepository,
            IAccountTableRepository accountTableRepository,
            IStockPriceTableRepository stockPriceTableRepository,
            IStockOfClientTableRepository stockOfClientTableRepository,
            ILogger logger)
        {
            this.clientsTableRepository = clientsTableRepository;
            this.accountTableRepository = accountTableRepository;
            this.stockPriceTableRepository = stockPriceTableRepository;
            this.stockOfClientTableRepository = stockOfClientTableRepository;
            this.logger = logger;
        }

        public void Register(RegInfo regInfo)
        {
            ClientInfo clientInfo = new ClientInfo()
            {
                Name = regInfo.Name,
                Surname = regInfo.Surname,
                PhoneNumber = regInfo.PhoneNumber
            };

            AccountInfo accountInfo = new AccountInfo()
            {
                ClientId = RegisterNewClient(clientInfo),
                Balance = regInfo.Balance
            };

            int accountId = CreateNewAccountForNewClient(accountInfo);

            if (regInfo.HaveStoks)
            {
                StockOfClientInfo stockOfClientInfo = new StockOfClientInfo()
                {
                    AccountId = accountId,
                    TypeOfStocks = regInfo.TypeOfStocks,
                    quantityOfStocks = regInfo.quantityOfStocks
                };

                if (!CheckIfStockPriseConteinStockOfClientByTypeOfStock(regInfo.TypeOfStocks))
                {
                    RegisterNewTypeOfStock(new StockPriceInfo()
                    {
                        TypeOfStock = regInfo.TypeOfStocks,
                        PriceOfStock = regInfo.PriceOfStock
                    });
                }
                RegisterStockForNewClient(stockOfClientInfo);
            }
        }

        public int RegisterNewClient(ClientInfo args)
        {
            var entityToAdd = new ClientEntity()
            {
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber
            };

            if (this.clientsTableRepository.GetClientByNameAndSurname(entityToAdd.Name, entityToAdd.Surname) != null)
            {
                throw new ArgumentException("This client already registered");
            }

            this.clientsTableRepository.Add(entityToAdd);
            this.clientsTableRepository.SaveChanges();
            logger.Info($"Client {entityToAdd.Id}, {entityToAdd.Name} {entityToAdd.Surname} has been registered");

            return entityToAdd.Id;
        }

        public int CreateNewAccountForNewClient(AccountInfo args)
        {
            var entityToAdd = new AccountEntity()
            {
                ClientId = args.ClientId,
                Balance = args.Balance,
            };

            if (entityToAdd.Balance > 0)
            {
                entityToAdd.Zone = "white";
            }

            if (entityToAdd.Balance == 0)
            {
                entityToAdd.Zone = "orange";
            }

            if (entityToAdd.Balance < 0)
            {
                entityToAdd.Zone = "black";
            }

            if (this.accountTableRepository.GetAccountByClientId(entityToAdd.ClientId) != null)
            {
                throw new ArgumentException("This client already have an account");
            }

            this.accountTableRepository.Add(entityToAdd);
            this.accountTableRepository.SaveChanges();
            logger.Info($"Account {entityToAdd.AccountId} for client {entityToAdd.ClientId} has been created");

            return entityToAdd.AccountId;
        }

        public void RegisterStockForNewClient(StockOfClientInfo args)
        {
            if (!CheckIfStockPriseConteinStockOfClientByTypeOfStock(args.TypeOfStocks))
            {
                throw new ArgumentException("StockPrice Table doesnt contain this type of stock");
            }

            var entityToAdd = new StockOfClientEntity()
            {
                AccountId = args.AccountId,
                TypeOfStocks = args.TypeOfStocks.ToUpper(),
                quantityOfStocks = args.quantityOfStocks
            };

            stockOfClientTableRepository.Add(entityToAdd);
            stockOfClientTableRepository.SaveChanges();
            logger.Info($"Stock {entityToAdd.Id} of type {entityToAdd.TypeOfStocks} in quantity {entityToAdd.quantityOfStocks} for account {entityToAdd.AccountId} has been added");
        }

        public bool CheckIfStockPriseConteinStockOfClientByTypeOfStock(string typeOfStock)
        {
            if (stockPriceTableRepository.GetStockPriceEntityByStockType(typeOfStock.ToUpper()) == null)
            {
                return false;
            }
            return true;
        }

        public void RegisterNewTypeOfStock(StockPriceInfo args)
        {
            if (CheckIfStockPriseConteinStockOfClientByTypeOfStock(args.TypeOfStock))
            {
                throw new ArgumentException("StockPrice Table already contain this type of stock");
            }

            var entityToAdd = new StockPriceEntity()
            {
                TypeOfStock = args.TypeOfStock.ToUpper(),
                PriceOfStock = args.PriceOfStock
            };

            stockPriceTableRepository.Add(entityToAdd);
            stockPriceTableRepository.SaveChanges();
            logger.Info($"Stock {entityToAdd.Id} of type {entityToAdd.TypeOfStock} with price {entityToAdd.PriceOfStock} has been created");
        }

        public void UpdateClient(ClientInfo clientInfo)
        {
            var clientToUpdate = this.clientsTableRepository.GetClientByNameAndSurname(clientInfo.Name, clientInfo.Surname);
            if (clientToUpdate == null)
            {
                throw new ArgumentException("This client doesnt exist");
            }

            clientToUpdate.Name = clientInfo.Name;
            clientToUpdate.Surname = clientInfo.Surname;
            clientToUpdate.PhoneNumber = clientInfo.PhoneNumber;

            clientsTableRepository.SaveChanges();
        }

        public void RemoveClient(ClientInfo clientInfo)
        {
            var clientToRemove = this.clientsTableRepository.GetClientByNameAndSurname(clientInfo.Name, clientInfo.Surname);
            if (clientToRemove == null)
            {
                throw new ArgumentException("This client doesnt exist");
            }
            clientsTableRepository.Remove(clientToRemove);

            clientsTableRepository.SaveChanges();
        }

        public void UpdateStock(StockOfClientInfo stockOfClientInfo)
        {
            var stockToUpdate = this.stockPriceTableRepository.GetStockPriceEntityByStockType(stockOfClientInfo.TypeOfStocks);
            if (stockToUpdate == null)
            {
                throw new ArgumentException("This stock doesnt exist");
            }

            stockToUpdate.PriceOfStock = stockOfClientInfo.PriceOfStock;

            stockPriceTableRepository.SaveChanges();
        }

        public void RemoveStock(StockOfClientInfo stockOfClientInfo)
        {
            var stockToRemove = this.stockPriceTableRepository.GetStockPriceEntityByStockType(stockOfClientInfo.TypeOfStocks);
            if (stockToRemove == null)
            {
                throw new ArgumentException("This stock doesnt exist");
            }

            var stocksOfClientsToRemove = this.stockOfClientTableRepository.GetAllStockOfClient().ToList();

            for (int i = 0; i < stocksOfClientsToRemove.Count; i++)
            {
                stockOfClientTableRepository.Remove(stocksOfClientsToRemove[i]);

            }
            stockOfClientTableRepository.SaveChanges();
            stockPriceTableRepository.Remove(stockToRemove);
            stockPriceTableRepository.SaveChanges();
        }
    }
}
