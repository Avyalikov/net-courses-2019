namespace TradingApp.Core.Repositories
{
    using System.Collections.Generic;
    using TradingApp.Core.Models;

    public interface ITransactionTableRepository
    {
        void Add(TransactionStoryEntity transactionStoryEntity);
        void SaveChanges();
        List<TransactionStoryEntity> GetTransactionsByUserId(int userId);
    }
}

