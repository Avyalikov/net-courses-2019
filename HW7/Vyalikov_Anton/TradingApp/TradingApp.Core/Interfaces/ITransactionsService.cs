namespace TradingApp.Core.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface ITransactionsService
    {
        void AddTransaction(Transaction transaction);
        void SellOrBuyShares(Transaction transaction);
        IEnumerable<Transaction> GetAllTransactions();
    }
}
