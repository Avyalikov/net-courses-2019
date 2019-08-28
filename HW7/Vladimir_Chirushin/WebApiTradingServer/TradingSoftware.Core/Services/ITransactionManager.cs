namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface ITransactionManager
    {
        void AddTransaction(int sellerID, int buyerID, int stockID, int stockAmount);

        void AddTransaction(Transaction transaction);

        bool Validate(Transaction transaction);

        void TransactionAgent(Transaction transaction);

        IEnumerable<Transaction> GetAllTransactions();
    }
}