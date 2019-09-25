namespace TradeSimulator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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

        public ICollection<HistoryEntity> GetFullHistory()
        {
            return historyTableRepository.GetHistory();
        }
        public ICollection<ClientEntity> GetAllClients()
        {
            return clientsTableRepository.GetAllClients();
        }
        public ICollection<StockPriceEntity> GetAllStockPrice()
        {
            return stockPriceTableRepository.GetAllStockPrice();
        }
        public StockPriceEntity GetStockPriceByType(string stockType)
        {
            return stockPriceTableRepository.GetStockPriceEntityByStockType(stockType);
        }
        public ClientEntity GetClientById(int clientId)
        {
            return clientsTableRepository.GetClientById(clientId);
        }
        public ClientEntity GetClientByNameSurname(string name, string surname)
        {
            return clientsTableRepository.GetClientByNameAndSurname(name, surname);
        }
        public AccountEntity GetAccountByClientId(int clientId)
        {
            return accountTableRepository.GetAccountByClientId(clientId);
        }
        public ICollection<StockOfClientEntity> GetAllStocksOfClient()
        {
            return stockOfClientTableRepository.GetAllStockOfClient().ToList();
        }
        public ICollection<StockOfClientEntity> GetStocksOfClientByAccountId(int accountId)
        {
            return stockOfClientTableRepository.GetFullStockOfClientByAccountId(accountId).ToList();
        }
        public ICollection<ClientEntity> GetClientsInOrangeZone()
        {
            return clientsTableRepository.GetAllClients().Where(w => accountTableRepository.GetAccountByClientId(w.Id).Zone == "orange").ToList();
        }
        public ICollection<ClientEntity> GetClientsInBlackZone()
        {
            return clientsTableRepository.GetAllClients().Where(w => accountTableRepository.GetAccountByClientId(w.Id).Zone == "black").ToList();
        }
    }
}
