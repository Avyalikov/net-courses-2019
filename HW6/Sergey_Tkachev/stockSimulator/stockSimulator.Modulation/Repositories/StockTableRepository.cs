namespace stockSimulator.Modulation.Repositories
{
    using System.Linq;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;

    internal class StockTableRepository : IStockTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        /// <summary>
        /// Initializes an Instance of StockTableRepository class.
        /// </summary>
        /// <param name="dbContext">Instance inheriting from DbContext.</param>
        public StockTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds new Stock into Database.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        public void Add(StockEntity entity)
        {
            this.dbContext.Stocks.Add(entity);
        }

        /// <summary>
        /// Check if such entity already exists in Database by its fields.
        /// </summary>
        /// <param name="entityToCheck">Entity to check.</param>
        /// <returns></returns>
        public bool Contains(StockEntity entityToCheck)
        {
            return this.dbContext.Stocks
               .Any(s => s.Name == entityToCheck.Name
               && s.Type == entityToCheck.Type
               && s.Cost == entityToCheck.Cost);
        }

        /// <summary>
        /// Check if such entity already exists in Database by its id.
        /// </summary>
        /// <param name="stockId">Entity's ID.</param>
        /// <returns></returns>
        public bool ContainsById(int stockId)
        {
            return this.dbContext.Stocks
               .Any(s => s.ID == stockId);
        }

        /// <summary>
        /// Returns a stock's entity by its ID.
        /// </summary>
        /// <param name="stockId">Stock's ID.</param>
        /// <returns></returns>
        public StockEntity Get(int stockId)
        {
            return this.dbContext.Stocks
                .Where(s => s.ID == stockId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns cost of Stock by its ID.
        /// </summary>
        /// <param name="stockId">Stock's ID.</param>
        /// <returns></returns>
        public decimal GetCost(int stockId)
        {
            return this.dbContext.Stocks
               .Where(s => s.ID == stockId)
               .Select(s => s.Cost)
               .FirstOrDefault();
        }

        /// <summary>
        /// Returns type of Stock by its ID.
        /// </summary>
        /// <param name="stockId">Stock's ID.</param>
        /// <returns></returns>
        public string GetType(int stockId)
        {
            return this.dbContext.Stocks
              .Where(s => s.ID == stockId)
              .Select(s => s.Type)
              .FirstOrDefault();
        }

        /// <summary>
        /// Saves changes in Database.
        /// </summary>
        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates stock's data by its ID.
        /// </summary>
        /// <param name="stockId">Stock's ID.</param>
        /// <param name="entityToUpdate">Entity with new data.</param>
        public void Update(int stockId, StockEntity entityToUpdate)
        {
            var clientToUpdate = this.dbContext.Stocks.First(s => s.ID == stockId);
            clientToUpdate = entityToUpdate;
        }
    }
}