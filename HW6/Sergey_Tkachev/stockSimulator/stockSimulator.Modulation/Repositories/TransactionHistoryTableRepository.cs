namespace stockSimulator.Modulation.Repositories
{
    using System.Linq;
    using stockSimulator.Core.Repositories;

    internal class TransactionHistoryTableRepository : ITransactionHistoryTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        /// <summary>
        /// Initializes an Instance of TransactionHistoryTableRepository class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public TransactionHistoryTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Adds HistoryEntity to DataBase.
        /// </summary>
        /// <param name="entity">HistoryEntity to add.</param>
        public void Add(HistoryEntity entity)
        {
            this.dbContext.TransactionHistory.Add(entity);
        }

        /// <summary>
        /// Checks if HistoryEntity exists by its fields.
        /// </summary>
        /// <param name="entityToCheck">Entity to check.</param>
        /// <returns></returns>
        public bool Contains(HistoryEntity entityToCheck)
        {
            return this.dbContext.TransactionHistory
               .Any(t => t.CustomerID == entityToCheck.CustomerID
               && t.SellerID == entityToCheck.SellerID
               && t.StockID == entityToCheck.StockID
               && t.StockAmount == entityToCheck.StockAmount
               && t.TransactionCost == entityToCheck.TransactionCost);
        }

        /// <summary>
        /// Checks if HistoryEntity exists by id.
        /// </summary>
        /// <param name="historyId">ID of entity to check.</param>
        /// <returns></returns>
        public bool ContainsById(int historyId)
        {
            return this.dbContext.TransactionHistory
               .Any(t => t.ID == historyId);
        }

        /// <summary>
        /// Returns HistoryEntity by id.
        /// </summary>
        /// <param name="historyId">ID of entity.</param>
        /// <returns></returns>
        public HistoryEntity Get(int historyId)
        {
            return this.dbContext.TransactionHistory
               .Where(t => t.ID == historyId)
               .FirstOrDefault();
        }

        /// <summary>
        /// Saves changes.
        /// </summary>
        public void SaveChanges()
        {
           this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates entity in TransactionHistoryTable.
        /// </summary>
        /// <param name="historyId">ID of entity to edit.</param>
        /// <param name="entityToEdit">New edited data.</param>
        public void Update(int historyId, HistoryEntity entityToEdit)
        {
            var clientToUpdate = this.dbContext.TransactionHistory.First(t => t.ID == historyId);
            clientToUpdate = entityToEdit;
        }
    }
}