using stockSimulator.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace stockSimulator.WevServer.Repositories
{
    class TransactionHistoryTableRepository : ITransactionHistoryTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        public TransactionHistoryTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(HistoryEntity entity)
        {
            this.dbContext.TransactionHistory.Add(entity);
        }

        public bool Contains(HistoryEntity entityToCheck)
        {
            return this.dbContext.TransactionHistory
               .Any(t => t.CustomerID == entityToCheck.CustomerID
               && t.SellerID == entityToCheck.SellerID
               && t.StockID == entityToCheck.StockID
               && t.StockAmount == entityToCheck.StockAmount
               && t.TransactionCost == entityToCheck.TransactionCost);
        }

        public bool ContainsByClientId(int clientId)
        {
            return this.dbContext.TransactionHistory
               .Any(t => t.CustomerID == clientId
               || t.SellerID == clientId);
        }

        public bool ContainsById(int historyId)
        {
            return this.dbContext.TransactionHistory
               .Any(t => t.ID == historyId);
        }


        public HistoryEntity Get(int historyId)
        {
            return this.dbContext.TransactionHistory
               .Where(t => t.ID == historyId)
               .FirstOrDefault();
        }

        public IEnumerable<HistoryEntity> GetClientsTransactions(int clientId, int top)
        {
            return this.dbContext.TransactionHistory
                .Where(th => th.SellerID == clientId
                || th.CustomerID == clientId)
                .Take(top);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Update(int historyId, HistoryEntity entityToEdit)
        {
            var clientToUpdate = this.dbContext.TransactionHistory.First(t => t.ID == historyId);
            clientToUpdate = entityToEdit;
        }
    }
}
