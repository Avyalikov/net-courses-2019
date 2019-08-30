namespace TradingSimulator.DataBase.Repositories
{
    using Core.Dto;
    using DataBase.Model;
    using System.Collections.Generic;

    internal static class RepositoriesExtension
    {
        internal static Trader ToTrader(this TraderEntity traderEntity) =>
            traderEntity == null ? null : new Trader()
            {
                Id = traderEntity.ID,
                name = traderEntity.Card.Name,
                surname = traderEntity.Card.Surname,
                phone = traderEntity.Card.Phone,
                money = traderEntity.Money,
            };

        internal static Share ToShare(this ShareEntity shareEntity) =>
            shareEntity == null ? null : new Share()
            {
                id = shareEntity.ID,
                name = shareEntity.Name,
                price = shareEntity.Price,
                quantity = shareEntity.Quantity,
                ownerId = shareEntity.Owner.ID,
            };

        internal static Transaction ToTransaction(this TransactionEntity transactionEntity) =>
            transactionEntity == null ? null : new Transaction()
            {
                seller = transactionEntity.Seller.ToTrader(),
                buyer = transactionEntity.Buyer.ToTrader(),
                sellerShare = new Share()
                {
                    name = transactionEntity.ShareName,
                    price = transactionEntity.SharePrice,
                    quantity = transactionEntity.ShareQuantity,
                    ownerId = transactionEntity.Seller.ID,
                },
                buyerShare = new Share()
                {
                    name = transactionEntity.ShareName,
                    price = transactionEntity.SharePrice,
                    quantity = transactionEntity.ShareQuantity,
                    ownerId = transactionEntity.Buyer.ID,
                },
            };

        internal static List<Trader> ToTrader(this List<TraderEntity> tradersEntity)
        {
            var traders = new List<Trader>();
            foreach (var trader in tradersEntity)
            {
                traders.Add(trader.ToTrader());
            }

            return traders;
        }

        internal static List<Share> ToShare(this List<ShareEntity> sharesEntity)
        {
            var shares = new List<Share>();
            foreach (var share in sharesEntity)
            {
                shares.Add(share.ToShare());
            }

            return shares;
        }

        internal static List<Transaction> ToTransaction(this List<TransactionEntity> transactionsEntity)
        {
            var transactions = new List<Transaction>();
            foreach (var transaction in transactionsEntity)
            {
                transactions.Add(transaction.ToTransaction());
            }

            return transactions;
        }
    }
}