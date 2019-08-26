using stockSimulator.Core.Repositories;

namespace stockSimulator.Modulation.Repositories
{
    class TransactionHistoryTableRepository : ITransactionHistoryTableRepository
    {
        public void Add(HistoryEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(HistoryEntity entityToAdd)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsById(int stockId)
        {
            throw new System.NotImplementedException();
        }

        public HistoryEntity Get(int stockId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void Update(HistoryEntity entityToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}