using HW7.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Repositories
{
    public interface ITransactionsRepository
    {
        IQueryable<Transaction> GetNumberOfTransactionsForTrader(int TraderId, int numberOfTransactions);
        Transaction AddTransaction(int buyerId, int sellerId, int shareId, decimal sharePrice, int purchaseQuantity);
    }
}
