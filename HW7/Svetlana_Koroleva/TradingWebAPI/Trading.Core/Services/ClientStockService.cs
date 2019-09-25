
using Trading.Core.Model;
using Trading.Core.Repositories;
using Trading.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.IServices;

namespace Trading.Core.Services
{
    public class ClientStockService:IClientStockService
    {
        private readonly IUnitOfWork unitOfWork;
        public ClientStockService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void AddClientStockToDB(ClientStockInfo args)
        {
            var clientstockToAdd = new ClientStock()
            {
                ClientID = args.ClientId,
                StockID = args.StockId,
                Quantity = args.Amount

            };
            if (this.unitOfWork.ClientStocks.Get(c=>c.ClientID==args.ClientId&&c.StockID==args.StockId).Count()!=0)
            {
                throw new ArgumentException("This clientstock exists. Can't continue");
            };

            this.unitOfWork.ClientStocks.Add(clientstockToAdd);
            this.unitOfWork.Save();

        }

        public ClientStock GetEntityByCompositeID(int clientId, int stockId)
        {
            if (this.unitOfWork.ClientStocks.Get(c=>c.StockID==stockId&&c.ClientID==clientId).Count()==0)
            {
                throw new ArgumentException("Clientstock doesn't exist");
            }
            return this.unitOfWork.ClientStocks.Get(c => c.StockID == stockId && c.ClientID == clientId).Single();
        }

        public void EditClientStocksAmount(int clientId, int stockId, int amountToAdd)
        {

            if (this.unitOfWork.ClientStocks.Get(c => c.StockID == stockId && c.ClientID == clientId).Count()==0)
            {
                AddClientStockToDB(new ClientStockInfo()
                {
                    ClientId = clientId,
                    StockId = stockId,
                    Amount = amountToAdd
                });
            }

            else
            {
                ClientStock clientStock = this.GetEntityByCompositeID(clientId, stockId);
                clientStock.Quantity += amountToAdd;
                this.unitOfWork.ClientStocks.Update(clientStock);
                this.unitOfWork.Save();
            }
            
        }

        public void Delete(int clientId, int stockId)
        {
            var clientStockToDelete = this.GetEntityByCompositeID(clientId, stockId);
            this.unitOfWork.ClientStocks.Delete(clientStockToDelete);
            this.unitOfWork.Save();
        }

        public void Update(int clientId, int stockId,ClientStockInfo clientStockInfo)
        {
            var stockToUpdate = this.GetEntityByCompositeID(clientId, stockId);
            stockToUpdate.Quantity = clientStockInfo.Amount;
            this.unitOfWork.ClientStocks.Update(stockToUpdate);
            this.unitOfWork.Save();
        }
        public IQueryable GetClientStocksWithPrice(int clientId)
        {
            var clientstocks = this.unitOfWork.ClientStocks.Get(c=>c.ClientID==clientId).ToList();
            var stocks = this.unitOfWork.Stocks.GetAll().ToList();
            var result = from cs in clientstocks
                               join s in stocks on cs.StockID equals s.StockID
                   select new {s.Issuer, s.StockPrefix, s.StockType, s.Price, cs.Quantity };

           
            return result.AsQueryable();    
                     
        }

        public IEnumerable<ClientStock> GetclientStocks(int clientId)
        {
                    
            return this.unitOfWork.ClientStocks.Get(c => c.ClientID == clientId).ToList();

        }

    }
}
