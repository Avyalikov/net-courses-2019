namespace TradingSoftware.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;

    public interface ITransactionRepository
    {
        void Insert(Transaction transaction);

        IQueryable<Transaction> GetQueryableTransactions();

        IEnumerable<Transaction> GetAllTransaction();

        IEnumerable<Transaction> GetTransactionWithClient(int clientID);
    }
}