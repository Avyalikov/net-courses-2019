namespace TradingApp.View.Repositories
{
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

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
