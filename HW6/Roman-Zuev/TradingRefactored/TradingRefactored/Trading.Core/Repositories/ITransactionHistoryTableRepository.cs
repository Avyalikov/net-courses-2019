using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface ITransactionHistoryTableRepository
    {
        void Add(TransactionHistoryEntity transaction);
        void SaveChanges();
    }
}