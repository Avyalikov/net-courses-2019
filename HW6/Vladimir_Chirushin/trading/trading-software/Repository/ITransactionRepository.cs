namespace trading_software
{
    using System.Collections.Generic;

    public interface ITransactionRepository
    {
        bool Add(Transaction transaction);
        IEnumerable<Transaction> GetAllTransaction();
    }
}