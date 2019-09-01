namespace TradingSimulator.DataBase.Repositories
{
    using Core.Dto;
    using Core.Repositories;
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public class TransactionRepository : ITransactionRepository
    {
        private readonly TradingDbContext db;
        public TransactionRepository(TradingDbContext db) =>
            this.db = db;

        public List<Transaction> GetTransactions(int traderId, int top) =>
            this.db.Transactions
            .Where(tr => tr.Seller.ID == traderId || tr.Buyer.ID == traderId)
            .Take(top)
            .ToList()
            .ToTransaction();

        public List<Transaction> GetTransactions() =>
            this.db.Transactions
            .ToList()
            .ToTransaction();

        public void Push(Transaction transaction)
        {
            var seller = db.Traders
                .Where(t => t.ID == transaction.seller.Id)
                .Single();
            var buyer = db.Traders
                .Where(t => t.ID == transaction.buyer.Id)
                .Single();

            db.Transactions.Add(
                new TransactionEntity()
                {
                    Seller = seller,
                    Buyer = buyer,
                    ShareName = transaction.buyerShare.name,
                    SharePrice = transaction.buyerShare.price,
                    ShareQuantity = transaction.buyerShare.quantity,
                });
        }

        public void SaveChanges() => this.db.SaveChanges();
    }
}