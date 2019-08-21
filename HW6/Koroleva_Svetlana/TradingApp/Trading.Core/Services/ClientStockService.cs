
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
        //private readonly ITableRepository tableRepository;
        private readonly ILinkedTableRepository linkedTableRepository;

        public ClientStockService( ILinkedTableRepository linkedTableRepository)
        {
            
            this.linkedTableRepository =linkedTableRepository;
        }
        public void AddClientStock(ClientStockInfo args)
            {
               ClientStock clientStock = new ClientStock() { Quantity = args.Amount };
               this.linkedTableRepository.Add(clientStock);
               this.linkedTableRepository.SaveChanges();

            }

        public ClientStock GetEntityByCompositeID(int clientId, int stockId)
        {
            if (!this.linkedTableRepository.ContainsByCompositeID(clientId, stockId))
            {
                throw new ArgumentException("Client doesn't exist");
            }
            return (ClientStock)this.linkedTableRepository.GetEntityByCompositeID(clientId, stockId);
        }

        public void EditClientStocksAmount(int clientId, int stockId, int amountToAdd)
        {
            ClientStock clientStock=this.GetEntityByCompositeID(clientId, stockId);
            clientStock.Quantity += amountToAdd;
            this.linkedTableRepository.SaveChanges();

            

        }
       /* public ClientStock GetRandomClientStock(int clientID)
        {
            Random random = new Random();
            ClientStock clstock = null;
            var clstocks = db.ClientStocks.Where(c=>c.ClientID==clientID).Select(o => o).ToList();
            if (clstocks == null)
            {
                throw new NullReferenceException("No stocks");
            }
            int number = random.Next(clstocks.Count() - 1);
                clstock = clstocks[number];
            return clstock;
        }*/
    }
}
