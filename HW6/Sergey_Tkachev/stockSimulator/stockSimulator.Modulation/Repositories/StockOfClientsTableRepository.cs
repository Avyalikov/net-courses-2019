namespace stockSimulator.Modulation.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;

    internal class StockOfClientsTableRepository : IStockOfClientsTableRepository
    {
        private readonly StockSimulatorDbContext DbContext;

        /// <summary>
        /// Initializes an Instance of StockOfClientsTableRepository class.
        /// </summary>
        /// <param name="DbContext">Instance inheriting from DbContext.</param>
        public StockOfClientsTableRepository(StockSimulatorDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        /// <summary>
        /// Adds StockOfClientsEntity into database.
        /// </summary>
        /// <param name="entity">Entity toa dd.</param>
        public void Add(StockOfClientsEntity entity)
        {
            this.DbContext.StockOfClients.Add(entity);
        }

        /// <summary>
        /// Checks if such StockOfClientsEntity already contains in Database by its fiels and if it's true returns its ID.
        /// </summary>
        /// <param name="entityToCheck">Entity to check.</param>
        /// <param name="entityId">Entity's ID.</param>
        /// <returns></returns>
        public bool Contains(StockOfClientsEntity entityToCheck, out int entityId)
        {
            var entry = this.DbContext.StockOfClients
              .FirstOrDefault(sc => sc.ClientID == entityToCheck.ClientID
              && sc.StockID == entityToCheck.StockID);
            if (entry != null)
            {
                entityId = entry.ID;
                return true;
            }

            entityId = 0;
            return false;
        }

        /// <summary>
        /// Checks if such StockOfClientsEntity already contains in Database by its ID.
        /// </summary>
        /// <param name="entityId">Entity's ID.</param>
        /// <returns></returns>
        public bool ContainsById(int entityId)
        {
            return this.DbContext.StockOfClients
              .Any(sc => sc.ID == entityId);
        }

        /// <summary>
        /// Returns StockOfClientsEntity by its ID.
        /// </summary>
        /// <param name="entityId">Entity's ID.</param>
        /// <returns></returns>
        public StockOfClientsEntity Get(int entityId)
        {
            return this.DbContext.StockOfClients
               .Where(sc => sc.ID == entityId)
               .FirstOrDefault();
        }

        /// <summary>
        /// Returns Amount of Client's stocks.
        /// </summary>
        /// <param name="client_id">Client's ID</param>
        /// <param name="stockId">Stock's ID.</param>
        /// <returns></returns>
        public int GetAmount(int client_id, int stockId)
        {
            return this.DbContext.StockOfClients
               .Where(sc => sc.ClientID == client_id
               && sc.StockID == stockId)
               .Select(soc => soc.Amount)
               .FirstOrDefault();
        }

        /// <summary>
        /// Returns collection of client's stocks.
        /// </summary>
        /// <param name="clientId">Client's ID.</param>
        /// <returns></returns>
        public IQueryable<StockOfClientsEntity> GetStocksOfClient(int clientId)
        {
            var retListOfStocksOfClient = this.DbContext.StockOfClients
                .Where(sc => sc.ClientID == clientId)
                .Include(sc => sc.Stock)
                .Include(sc => sc.Client);

            return retListOfStocksOfClient;
        }

        /// <summary>
        /// Save changes in Database.
        /// </summary>
        public void SaveChanges()
        {
            this.DbContext.SaveChanges();
        }

        /// <summary>
        /// Updates StockOfClientsEntity data.
        /// </summary>
        /// <param name="entityId">Entity's ID.</param>
        /// <param name="newEntity">Entity with new data.</param>
        /// <returns></returns>
        public string Update(int entityId, StockOfClientsEntity newEntity)
        {
            var stockOfCloentToUpdate = this.DbContext.StockOfClients.FirstOrDefault(c => c.ID == entityId);
            if (stockOfCloentToUpdate != null)
            {
                stockOfCloentToUpdate.Amount = newEntity.Amount;
                this.SaveChanges();
                return "Stock of Client data was updated.";
            }

            return "Stock of Client data wasn't found.";
        }

        /// <summary>
        /// Updates amount of client's stocks.
        /// </summary>
        /// <param name="client_id">Client's ID.</param>
        /// <param name="stockId">Stock's ID.</param>
        /// <param name="newStockAmount">New stock's amount.</param>
        public void UpdateAmount(int client_id, int stockId, int newStockAmount)
        {
            var entityToUpdate = this.DbContext.StockOfClients
                .First(sc => sc.ClientID == client_id
               && sc.StockID == stockId);
            entityToUpdate.Amount = newStockAmount;
        }
    }
}