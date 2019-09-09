using Trading.TradesEmulator.Models;

namespace Trading.TradesEmulator.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.TradesEmulator.Models;
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