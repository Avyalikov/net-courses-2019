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

        public TransactionRepository(TradingDbContext db)
        {
            this.db = db;
        }

        public List<Transaction> GetTransactions()
        {
            var transactionsEntity = this.db.Transactions.ToList();

            List<Transaction> temp = new List<Transaction>();

            foreach (var transaction in transactionsEntity)
            {
                temp.Add(new Transaction()
                {
                    seller = new Trader()
                    {
                        Id = transaction.Seller.ID,
                        name = transaction.Seller.Card.Name,
                        surname = transaction.Seller.Card.Surname,
                        phone = transaction.Buyer.Card.Phone,
                        money = transaction.Seller.Money,
                    },
                    buyer = new Trader()
                    {
                        Id = transaction.Buyer.ID,
                        name = transaction.Buyer.Card.Name,
                        surname = transaction.Buyer.Card.Surname,
                        phone = transaction.Buyer.Card.Phone,
                        money = transaction.Buyer.Money,
                    },
                    sellerShare = new Share()
                    {
                        name = transaction.ShareName,
                        price = transaction.SharePrice,
                        quantity = transaction.ShareQuantity,
                        ownerId = transaction.Seller.ID,
                    },
                    buyerShare = new Share()
                    {
                        name = transaction.ShareName,
                        price = transaction.SharePrice,
                        quantity = transaction.ShareQuantity,
                        ownerId = transaction.Buyer.ID,
                    },
                });
            }

            return temp;
        }

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