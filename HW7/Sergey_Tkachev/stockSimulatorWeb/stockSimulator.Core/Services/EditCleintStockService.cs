using System;
using System.Collections.Generic;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;

namespace stockSimulator.Core.Services
{
    public class EditCleintStockService
    {
        private readonly IStockOfClientsTableRepository stockOfClientsTableRepository;

        public EditCleintStockService(IStockOfClientsTableRepository stockOfClientsTableRepository)
        {
            this.stockOfClientsTableRepository = stockOfClientsTableRepository;
        }

        public string Edit(EditStockOfClientInfo editArgs)
        {
            int entityId;
            string result = String.Empty;
            var entityToEdit = new StockOfClientsEntity()
            {
               ClientID = editArgs.Client_ID,
               StockID = editArgs.Stock_ID,
               Amount = editArgs.AmountOfStocks
            };

            if (this.stockOfClientsTableRepository.Contains(entityToEdit, out entityId))
            {
                result = this.stockOfClientsTableRepository.Update(entityId, entityToEdit);
            }
            else
            {
                this.stockOfClientsTableRepository.Add(entityToEdit);
                result = $"Entry for client {entityToEdit.ClientID} was added into DB";
                //throw new ArgumentException("There is no entry in DataBase, may be you eentered wrong userID or stockID");
            }

            this.stockOfClientsTableRepository.SaveChanges();

            return result;
        }

        public IEnumerable<StockOfClientsEntity> GetStocksOfClient(int clientId)
        {
            return stockOfClientsTableRepository.GetStocksOfClient(clientId);
        }

        public string addStock(EditStockOfClientInfo addInfo)
        {
            int entityId;
            var entityToAdd = new StockOfClientsEntity()
            {
                ClientID = addInfo.Client_ID,
                StockID = addInfo.Stock_ID,
                Amount = addInfo.AmountOfStocks
            };

            if (this.stockOfClientsTableRepository.Contains(entityToAdd, out entityId))
            {
                throw new ArgumentException("This client has already owned this stock, please use update request or select anothers parameters.");
            }

            this.stockOfClientsTableRepository.Add(entityToAdd);

            this.stockOfClientsTableRepository.SaveChanges();

            stockOfClientsTableRepository.Contains(entityToAdd, out entityId);

            return entityId.ToString();
        }

        public string Remove(EditStockOfClientInfo removeInfo)
        {

            int entityId;
            string result = String.Empty;
            var entityToRemove = new StockOfClientsEntity()
            {
                ClientID = removeInfo.Client_ID,
                StockID = removeInfo.Stock_ID
            };

            if (this.stockOfClientsTableRepository.Contains(entityToRemove, out entityId))
            {
                result = this.stockOfClientsTableRepository.Remove(entityId, entityToRemove);
                return result;
            }
            throw new ArgumentException($"Entry to remove with such clientID {removeInfo.Client_ID} and StockID {removeInfo.Stock_ID} wasn't found");
        }
    }
}
