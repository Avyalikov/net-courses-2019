
namespace TradingSimulator.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Dto;
    using Interfaces;
    using Repositories;

    public class TransactionService : ITransactionService
    {
        private readonly ITraderRepository traderRep;
        private readonly IShareRepository shareRep;
        private readonly ILoggerService logger;
        private readonly ITransactionRepository transactionRep;

        public TransactionService(
            ITraderRepository traderRep,
            IShareRepository shareRep,
            ITransactionRepository transactionRep,
            ILoggerService logger)
        {
            this.traderRep = traderRep;
            this.shareRep = shareRep;
            this.logger = logger;
            this.transactionRep = transactionRep;
        }

        public List<Transaction> GetTransactions()
        {
            return this.transactionRep.GetTransactions();
        }

        public List<Transaction> GetTransactions(int traderId, int top)
        {
            return this.transactionRep.GetTransactions(traderId, top);
        }

        public Transaction MakeDeal(int sellerId, int buyerId, string shareName, int quantity)
        {
            var seller = this.traderRep.GetTrader(sellerId);
            var buyer = this.traderRep.GetTrader(buyerId);

            var sellerShare = this.shareRep.GetShare(sellerId, shareName);
            if (sellerShare == null)
            {
                throw new NullReferenceException($"Can't find {shareName} share at trader with ID {sellerId}");
            }

            var buyerShare = this.shareRep.GetShare(buyerId, shareName);

            sellerShare.quantity -= quantity;
            if (buyerShare != null)
            {
                buyerShare.quantity += quantity;
            }
            else
            {
                buyerShare = new Share()
                {
                    name = sellerShare.name,
                    price = sellerShare.price,
                    quantity = quantity,
                    ownerId = buyer.Id,
                };
            }

            seller.money += (sellerShare.price * quantity);
            buyer.money -= (sellerShare.price * quantity);

            return new Transaction()
            {
                seller = seller,
                buyer = buyer,
                sellerShare = sellerShare,
                buyerShare = buyerShare
            };
        }

        public void Save(Transaction transaction)
        {
            if (transaction             == null ||
                transaction.seller      == null || 
                transaction.buyer       == null || 
                transaction.sellerShare == null ||
                transaction.buyerShare  == null)
            {
                throw new NullReferenceException("Transaction must not be null or continence field with null");
            }

            this.traderRep.Push(transaction.buyer);
            this.traderRep.Push(transaction.seller);

            shareRep.Push(transaction.buyerShare);
            switch (transaction.sellerShare.quantity)
            {
                case 0:
                    shareRep.Remove(transaction.sellerShare);
                    break;
                default:
                    shareRep.Push(transaction.sellerShare);
                    break;
            }

            transactionRep.Push(transaction);

            transactionRep.SaveChanges();

            logger.Info(transaction.ToString());
        }
    }
}