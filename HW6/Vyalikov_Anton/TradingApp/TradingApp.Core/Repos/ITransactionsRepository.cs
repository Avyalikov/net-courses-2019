namespace TradingApp.Core.Repos
{
    using System;
    using Models;
    using System.Collections.Generic;
    public interface ITransactionsRepository : IDBComm
    {
        void Insert(Transaction transaction);
        Transaction GetTransactionByID(int transactionID);
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetTransactionsByDate(DateTime transactionDate);
    }
}
