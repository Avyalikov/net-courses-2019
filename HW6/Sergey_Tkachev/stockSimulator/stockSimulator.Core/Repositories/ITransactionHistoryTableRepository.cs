using stockSimulator.Core.Models;

namespace stockSimulator.Core.Repositories
{
    public interface ITransactionHistoryTableRepository
    {
        void Add(HistoryEntity entity);
        void SaveChanges();
        bool Contains(HistoryEntity entityToAdd);
        HistoryEntity Get(int stockId);
        bool ContainsById(int stockId);
        void Update(HistoryEntity entityToUpdate);
    }
}
