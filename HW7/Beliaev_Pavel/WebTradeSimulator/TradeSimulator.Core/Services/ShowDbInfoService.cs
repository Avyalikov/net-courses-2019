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

    public class ShowDbInfoService
    {
        private readonly IClientsTableRepository clientsTableRepository;
        private readonly IAccountTableRepository accountTableRepository;
        private readonly IStockPriceTableRepository stockPriceTableRepository;
        private readonly IHistoryTableRepository historyTableRepository;
        private readonly IStockOfClientTableRepository stockOfClientTableRepository;

        public ShowDbInfoService(
            IClientsTableRepository clientsTableRepository,
            IAccountTableRepository accountTableRepository,
            IStockPriceTableRepository stockPriceTableRepository,
            IHistoryTableRepository historyTableRepository,
            IStockOfClientTableRepository stockOfClientTableRepository)
        {
            this.clientsTableRepository = clientsTableRepository;
            this.accountTableRepository = accountTableRepository;
            this.stockPriceTableRepository = stockPriceTableRepository;
            this.historyTableRepository = historyTableRepository;
            this.stockOfClientTableRepository = stockOfClientTableRepository;
        }

        public ICollection<HistoryInfo> GetNClientsHistoryRecords(int clientId, int quantityOfRecords)
        {
            if (quantityOfRecords <= 0)
            {
                throw new ArgumentException("Invalid number of records");
            }

            List<HistoryInfo> historyInfos = new List<HistoryInfo>();
            List<HistoryEntity> historyEntities = historyTableRepository.GetHistory().Where(w => (w.BuyerId == clientId) || (w.SellerId == clientId)).ToList();
            for (int i = 0; (i < quantityOfRecords) && (i < historyEntities.Count) ; i++)
            {
                historyInfos.Add(new HistoryInfo()
                {
                    Id = historyEntities[i].Id,
                    BuyerId = historyEntities[i].BuyerId,
                    SellerId = historyEntities[i].SellerId,
                    TypeOfStock = historyEntities[i].TypeOfStock,
                    QuantityOfStocks = historyEntities[i].QuantityOfStocks,
                    FullPrice = historyEntities[i].FullPrice
                });
            }

            return historyInfos;
        }
        public ICollection<HistoryEntity> GetFullHistory()
        {            
            return historyTableRepository.GetHistory();
        }


        public ICollection<ClientInfo> GetPageOfClients(int lenghOfPage, int numberOfPage)
        {
            if (lenghOfPage <= 0)
            {
                throw new ArgumentException("Invalid lengh of page");
            }
            if (numberOfPage <= 0)
            {
                throw new ArgumentException("Invalid number of page");
            }
            List<ClientInfo> ClientInfos = new List<ClientInfo>();
            List<ClientEntity> ClientEntities = clientsTableRepository.GetAllClients().ToList();
            for (int i = ( (lenghOfPage * numberOfPage) - lenghOfPage); (i < (lenghOfPage * numberOfPage)) && (i < ClientEntities.Count) ; i++)
            {
                ClientInfos.Add(new ClientInfo()
                {
                    Id = ClientEntities[i].Id,
                    Name = ClientEntities[i].Name,
                    Surname = ClientEntities[i].Surname,
                    PhoneNumber = ClientEntities[i].PhoneNumber
                });
            }
            return ClientInfos;
        }
        public ICollection<ClientInfo> GetAllClientsInfos()
        {            
            List<ClientInfo> ClientInfos = new List<ClientInfo>();
            List<ClientEntity> ClientEntities = clientsTableRepository.GetAllClients().ToList();
            for (int i = 0; i < ClientEntities.Count ; i++)
            {
                ClientInfos.Add(new ClientInfo()
                {
                    Id = ClientEntities[i].Id,
                    Name = ClientEntities[i].Name,
                    Surname = ClientEntities[i].Surname,
                    PhoneNumber = ClientEntities[i].PhoneNumber
                });
            }
            return ClientInfos;
        }
        public ICollection<ClientEntity> GetAllClients()
        {            
            return clientsTableRepository.GetAllClients();
        }
        public ClientEntity GetClientById(int clientId)
        {
            return clientsTableRepository.GetClientById(clientId);
        }
        public ClientEntity GetClientByNameSurname(string name, string surname)
        {
            return clientsTableRepository.GetClientByNameAndSurname(name, surname);
        }
        public ClientInfo GetClientInfoByNameSurname(string name, string surname)
        {
            ClientEntity clientEntity = clientsTableRepository.GetClientByNameAndSurname(name, surname);
            ClientInfo clientInfo = new ClientInfo()
            {
                Id = clientEntity.Id,
                Name = clientEntity.Name,
                Surname = clientEntity.Surname,
                PhoneNumber = clientEntity.PhoneNumber
            };
            return clientInfo;
        }
        public ICollection<ClientEntity> GetClientsInOrangeZone()
        {
            return clientsTableRepository.GetAllClients().Where(w => accountTableRepository.GetAccountByClientId(w.Id).Zone == "orange").ToList();
        }
        public ICollection<ClientEntity> GetClientsInBlackZone()
        {
            return clientsTableRepository.GetAllClients().Where(w => accountTableRepository.GetAccountByClientId(w.Id).Zone == "black").ToList();
        }
        public bool GetCheckIfBlackZoneIsNotEmpty()
        {
            return clientsTableRepository.GetAllClients().Where(w => accountTableRepository.GetAccountByClientId(w.Id).Zone == "black").Any();
        }

        public AccountEntity GetAccountByClientId(int clientId)
        {
            return accountTableRepository.GetAccountByClientId(clientId);
        }
        public AccountInfo GetAccountInfoByClientId(int clientId)
        {
            AccountEntity accountEntity = accountTableRepository.GetAccountByClientId(clientId);
            AccountInfo accountInfo = new AccountInfo()
            {
                AccountId = accountEntity.AccountId,
                ClientId = accountEntity.ClientId,
                Balance = accountEntity.Balance,
                Zone = accountEntity.Zone
            };
            return accountInfo;
        }


        public ICollection<StockPriceEntity> GetAllStockPrice()
        {
            return stockPriceTableRepository.GetAllStockPrice();
        }
        public StockPriceEntity GetStockPriceByType(string stockType)
        {
            return stockPriceTableRepository.GetStockPriceEntityByStockType(stockType);
        }

        public ICollection<StockOfClientInfo> GetAllStocksOfClientWithPriceByClientId(int clientId)
        {
            List<StockOfClientInfo> stockOfClientInfos = new List<StockOfClientInfo>();
            List<StockOfClientEntity> StockOfClientEntities = stockOfClientTableRepository.GetFullStockOfClientByClientId(clientId).ToList();
            for (int i = 0; i < StockOfClientEntities.Count; i++)
            {
                stockOfClientInfos.Add(new StockOfClientInfo()
                {
                    Id = StockOfClientEntities[i].Id,
                    ClientId = accountTableRepository.GetClientIdByAccountId(StockOfClientEntities[i].AccountId),
                    AccountId = StockOfClientEntities[i].AccountId,
                    TypeOfStocks = StockOfClientEntities[i].TypeOfStocks,
                    quantityOfStocks = StockOfClientEntities[i].quantityOfStocks,
                    PriceOfStock = stockPriceTableRepository.GetStockPriceEntityByStockType(StockOfClientEntities[i].TypeOfStocks).PriceOfStock
                });
            }

            return stockOfClientInfos;
        }
        public ICollection<StockOfClientInfo> GetAllStocksOfClient()
        {
            List<StockOfClientInfo> stockOfClientInfos = new List<StockOfClientInfo>();
            List<StockOfClientEntity> StockOfClientEntities = stockOfClientTableRepository.GetAllStockOfClient().ToList();
            
            for (int i = 0; i < StockOfClientEntities.Count; i++)
            {
                stockOfClientInfos.Add(new StockOfClientInfo()
                {
                    Id = StockOfClientEntities[i].Id,
                    ClientId = accountTableRepository.GetClientIdByAccountId(StockOfClientEntities[i].AccountId),
                    AccountId = StockOfClientEntities[i].AccountId,
                    TypeOfStocks = StockOfClientEntities[i].TypeOfStocks,
                    quantityOfStocks = StockOfClientEntities[i].quantityOfStocks,
                    PriceOfStock = stockPriceTableRepository.GetStockPriceEntityByStockType(StockOfClientEntities[i].TypeOfStocks).PriceOfStock
                });
            }

            return stockOfClientInfos;
        }
        public ICollection<StockOfClientEntity> GetStocksOfClientByAccountId(int accountId)
        {
            return stockOfClientTableRepository.GetFullStockOfClientByAccountId(accountId).ToList();
        }
    }
}
