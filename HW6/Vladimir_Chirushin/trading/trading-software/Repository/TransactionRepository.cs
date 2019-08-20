namespace trading_software
{
    using System.Collections.Generic;
    using System.Linq;

    public class TransactionRepository : ITransactionRepository
    {
        public bool Add(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Transaction> GetAllTransaction()
        {
            using (var db = new TradingContext())
            {
                IEnumerable<Transaction> query = db.TransactionHistory.AsEnumerable<Transaction>().ToList();
                return query;
            }
        }
    }
}