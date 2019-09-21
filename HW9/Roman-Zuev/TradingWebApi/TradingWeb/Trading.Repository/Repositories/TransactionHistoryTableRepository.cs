namespace Trading.Repository.Repositories
{
    using System;
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.Repository.Context;
    using System.Collections;
    using System.Linq;
    using System.Collections.Generic;

    public class TransactionHistoryTableRepository : ITransactionHistoryTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public TransactionHistoryTableRepository(TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TransactionHistoryEntity transaction)
        {
            this.dbContext.TransactionHistories.Add(transaction);
        }

        public ICollection<TransactionHistoryEntity> GetTopById(int clientId, int top)
        {
            return this.dbContext.TransactionHistories
                .Where(h => h.Buyer.Id == clientId || h.Seller.Id == clientId)
                .Take(top).ToList();
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}