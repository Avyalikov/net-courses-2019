namespace WebApiTradingServer.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class TransactionRepository : ITransactionRepository
    {
        public void Insert(Transaction transaction)
        {
            using (var db = new TradingContext())
            {
                db.TransactionHistory.Add(transaction);
                db.SaveChanges();
            }
        }

        public IQueryable<Transaction> GetQueryableTransactions()
        {
            using (var db = new TradingContext())
            {
                return db.TransactionHistory;
            }
        }

        public IEnumerable<Transaction> GetAllTransaction()
        {
            using (var db = new TradingContext())
            {
                return db.TransactionHistory.AsEnumerable<Transaction>().ToList();
            }
        }


        public IEnumerable<Transaction> GetTransactionWithClient(int clientID)
        {
            using (var db = new TradingContext())
            {
                return db.TransactionHistory
                    .Where(t => t.BuyerID == clientID ||
                                t.SellerID == clientID)
                    .AsEnumerable<Transaction>()
                    .ToList();
            }
        }

    }
}
