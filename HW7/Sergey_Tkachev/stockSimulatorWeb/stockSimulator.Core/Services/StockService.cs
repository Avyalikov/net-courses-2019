using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System;

namespace stockSimulator.Core.Services
{
    public class StockService
    {
        private readonly IStockTableRepository stockTableRepository;

        public StockService(IStockTableRepository stockTableRepository)
        {
            this.stockTableRepository = stockTableRepository;
        }

        public int RegisterNewStock(StockRegistrationInfo args)
        {
            var entityToAdd = new StockEntity()
            {
                Name = args.Name,
                Type = args.Type,
                Cost = args.Cost
            };

            if (this.stockTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This client has been registered already. Can't continue");
            }

            this.stockTableRepository.Add(entityToAdd);

            this.stockTableRepository.SaveChanges();

            return entityToAdd.ID;
        }

        public StockEntity GetStock(int stockId)
        {
            if (!this.stockTableRepository.ContainsById(stockId))
            {
                throw new ArgumentException("Can't get stock by this ID. May be it has not been created.");
            }

            return this.stockTableRepository.Get(stockId);
        }
    }
}
