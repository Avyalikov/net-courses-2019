namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;

    public interface ITransactionManager
    {
        void AddTransaction(int sellerID, int buyerID, int stockID, int stockAmount);

        void AddTransaction(Transaction transaction);

        bool Validate(Transaction transaction);

        void TransactionAgent(Transaction transaction);

        IEnumerable<TransactionsFullData> GetAllTransactions();

        IEnumerable<TransactionsFullData> GetTransactionWithClient(int clientID);

        IQueryable<Transaction> GetQueryableTransactions();

        bool Make(TransactionsMakeData transactionsMakeData);
    }
}