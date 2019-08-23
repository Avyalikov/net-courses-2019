using System.Collections.Generic;
using TradingSoftware.Core.Models;

namespace TradingSoftware.Core.Repositories
{
    public interface ITransactionRepository
    {
        bool Insert(Transaction transaction);
        IEnumerable<Transaction> GetAllTransaction();
    }
}
