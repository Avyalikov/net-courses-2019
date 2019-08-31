using Trading.Repository.Context;

namespace Trading.Repository.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.Repository.Context;
    using System.Linq;
    public class TransactionHistoryTableRepository : ITransactionHistoryTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public TransactionHistoryTableRepository (TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TransactionHistoryEntity transaction)
        {
            this.dbContext.TransactionHistories.Add(transaction);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}