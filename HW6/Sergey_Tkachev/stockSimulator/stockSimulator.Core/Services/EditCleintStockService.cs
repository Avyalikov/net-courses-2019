using System;
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

        public int Edit(EditStockOfClientInfo editArgs)
        {
            var entityToEdit = new StockOfClientsEntity()
            {
               ClientID = editArgs.Client_ID,
               StockID = editArgs.Stock_ID,
               Amount = editArgs.AmountOfStocks
            };

            if (this.stockOfClientsTableRepository.Contains(entityToEdit))
            {
                this.stockOfClientsTableRepository.Update(entityToEdit);
            }
            else
            {
                this.stockOfClientsTableRepository.Add(entityToEdit);
            }

            this.stockOfClientsTableRepository.SaveChanges();

            return entityToEdit.ID;
        }
    }
}
