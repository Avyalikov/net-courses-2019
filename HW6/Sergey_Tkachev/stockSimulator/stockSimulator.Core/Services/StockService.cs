namespace stockSimulator.Core.Services
{
    using System;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;

    public class StockService
    {
        private readonly IStockTableRepository stockTableRepository;

        /// <summary>
        /// Creates an Instance of StockService class.
        /// </summary>
        /// <param name="stockTableRepository">Instance of implementing IStockTableRepository interface.</param>
        public StockService(IStockTableRepository stockTableRepository)
        {
            this.stockTableRepository = stockTableRepository;
        }

        /// <summary>
        /// Registers new Stock.
        /// </summary>
        /// <param name="args">Registration data.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Return stock entity by its id.
        /// </summary>
        /// <param name="stockId">ID of stock to return.</param>
        /// <returns></returns>
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
