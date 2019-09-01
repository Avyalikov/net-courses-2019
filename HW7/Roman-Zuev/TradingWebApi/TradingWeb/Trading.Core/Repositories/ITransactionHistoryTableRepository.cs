using System.Collections;
using System.Collections.Generic;
using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface ITransactionHistoryTableRepository
    {
        void Add(TransactionHistoryEntity transaction);
        ICollection<TransactionHistoryEntity> GetTopById(int clientId, int top);
        void SaveChanges();
    }
}