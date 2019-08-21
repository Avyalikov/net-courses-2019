
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Services
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
            var clientstockToAdd = new ClientStock()
        {
           ClientID=args.ClientId,
           StockID=args.StockId,
           Quantity=args.Amount
           
        };
            if (this.tableRepository.Contains(clientstockToAdd))
            {
                throw new ArgumentException("This clientstock exists. Can't continue");
            };

            this.tableRepository.Add(clientstockToAdd);
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

if (!this.tableRepository.ContainsByPK(clientId, stockId))
            {
                AddClientStock(new ClientStockInfo()
                {
                    ClientId=clientId,
                    StockId=stockId,
                    Amount=amountToAdd
                });
            }
            ClientStock clientStock = this.GetEntityByCompositeID(clientId, stockId);
            

                clientStock.Quantity += amountToAdd;
            this.tableRepository.SaveChanges();



        }

        public ClientStock GetRandomClientStock(int clientID)
        {
            Random random = new Random();
            var clientStocks = this.tableRepository.Where(clientID).ToList();
            int stocksAmount = clientStocks.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(0, stocksAmount-1);
            ClientStock clientStock = (ClientStock)clientStocks[number];

            return clientStock;
        }
    }
}
