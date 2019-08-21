
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Modifiers
{
    public class ClientStockService
    {
        private readonly ITableRepository tableRepository;


        public ClientStockService(ITableRepository tableRepository)
        {

            this.tableRepository = tableRepository;
        }
        public void AddClientStock(ClientStockInfo args)
        {
            ClientStock clientStock = new ClientStock() { Quantity = args.Amount };
            this.tableRepository.Add(clientStock);
            this.tableRepository.SaveChanges();

        }

        public ClientStock GetEntityByCompositeID(int clientId, int stockId)
        {
            if (!this.tableRepository.ContainsByPK(clientId, stockId))
            {
                throw new ArgumentException("Client doesn't exist");
            }
            return (ClientStock)this.tableRepository.Find(clientId, stockId);
        }

        public void EditClientStocksAmount(int clientId, int stockId, int amountToAdd)
        {
            ClientStock clientStock = this.GetEntityByCompositeID(clientId, stockId);
            clientStock.Quantity += amountToAdd;
            this.tableRepository.SaveChanges();



        }

        public ClientStock GetRandomClientStock(int clientID)
        {
            Random random = new Random();
            int stocksAmount = this.tableRepository.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(1, stocksAmount);
            ClientStock clientStock = (ClientStock)tableRepository.GetElementAt(number);

            return clientStock;
        }
    }
}
