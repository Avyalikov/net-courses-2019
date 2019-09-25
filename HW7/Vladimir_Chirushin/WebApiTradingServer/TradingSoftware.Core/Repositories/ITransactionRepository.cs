namespace TradingSoftware.Core.Repositories
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface ITransactionRepository
    {
        void Insert(Transaction transaction);

        IEnumerable<Transaction> GetAllTransaction();

        IEnumerable<Transaction> GetTransactionWithClient(int clientID);
    }
}