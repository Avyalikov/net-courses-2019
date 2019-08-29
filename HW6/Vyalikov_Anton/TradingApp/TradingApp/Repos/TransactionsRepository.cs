namespace TradingApp.Repos
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repos;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class TransactionsRepository : DBComm, ITransactionsRepository
    {
        private readonly DBContext dBContext;

        public TransactionsRepository(DBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Insert(Transactions transaction)
        {
            dBContext.Transactions.Add(transaction);
        }
        public Transactions GetTransactionByID(int transactionID)
        {
            return dBContext.Transactions.Where(t => t.TransactionID == transactionID).FirstOrDefault();
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return dBContext.Transactions;
        }

        public IEnumerable<Transactions> GetTransactionsByDate(DateTime transactionDate)
        {
            return dBContext.Transactions.Where(t => t.Date == transactionDate);
        }
    }
}
