namespace TradingApp.Core.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface ITransactionsService
    {
        void AddTransaction(Transactions transaction);
        void SellOrBuyShares(Transactions transaction);
        IEnumerable<Transactions> GetAllTransactions();
    }
}
