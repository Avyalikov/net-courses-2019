using HW7.Core;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW7.Server.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private IContextProvider context;

        public TransactionsRepository(IContextProvider context)
        {
            this.context = context;
        }

        public Transaction AddTransaction(int buyerId, int sellerId, int shareId, decimal sharePrice, int purchaseQuantity)
        {
            var transaction = context.Transactions.Add(new Transaction
            {
                BuyerId = buyerId,
                SellerId = sellerId,
                ShareId = shareId,
                PricePerShare = sharePrice,
                Quantity = purchaseQuantity,
                DateTime = DateTime.Now
            });

            context.SaveChanges();

            return transaction;
        }

        public IQueryable<Transaction> GetNumberOfTransactionsForTrader(int TraderId, int numberOfTransactions)
        {
            return context.Transactions.Where(x => x.BuyerId == TraderId || x.SellerId == TraderId).OrderBy(x => x.SellerId).
               ThenBy(x => x.BuyerId).Take(numberOfTransactions);
        }
    }
}
