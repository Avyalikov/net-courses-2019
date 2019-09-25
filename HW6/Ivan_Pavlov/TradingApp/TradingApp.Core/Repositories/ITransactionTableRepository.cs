namespace TradingApp.Core.Repositories
{
    using TradingApp.Core.Models;

    public interface ITransactionTableRepository
    {
        void Add(TransactionStoryEntity transactionStoryEntity);
        void SaveChanges();
    }
}

