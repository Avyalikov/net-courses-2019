namespace TradingApp.OwinHostApi.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class TransactionTableRepository : ITransactionTableRepository
    {
        private readonly TradingAppDb db;

        public TransactionTableRepository(TradingAppDb db)
        {
            this.db = db;
        }

        public void Add(TransactionStoryEntity entity)
        {
            this.db.TransactionsStory.Add(entity);
        }

        public List<TransactionStoryEntity> GetTransactionsByUserId(int userId)
        {
            return this.db.TransactionsStory.Where(t => t.SellerId == userId || t.CustomerId == userId).ToList();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
