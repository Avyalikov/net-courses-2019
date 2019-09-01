namespace TradingApp.Core.Repos
{
    using System;
    using Models;
    using System.Collections.Generic;
    public interface ITransactionsRepository : IDBComm
    {
        void Insert(Transactions transaction);
        Transactions GetTransactionByID(int transactionID);
        IEnumerable<Transactions> GetAllTransactions();
        IEnumerable<Transactions> GetTransactionsByDate(DateTime transactionDate);
    }
}
