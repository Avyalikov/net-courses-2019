namespace stockSimulator.Core.Repositories
{
    public interface ITransactionHistoryTableRepository
    {
        void Add(HistoryEntity entity);
        void SaveChanges();
        bool Contains(HistoryEntity entityToCheck);
        HistoryEntity Get(int historyId);
        bool ContainsById(int historyId);
        void Update(int historyId, HistoryEntity entityToEdit);
    }
}
