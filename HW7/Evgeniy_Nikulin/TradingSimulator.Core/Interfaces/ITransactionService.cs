namespace TradingSimulator.Core.Interfaces
{
    using System.Collections.Generic;
    using Dto;

    public interface ITransactionService
    {
        List<Transaction> GetTransactions();
        List<Transaction> GetTransactions(int traderId, int top);
        Transaction MakeDeal(int sellerId, int buyerId, string shareName, int quantity);
        void Save(Transaction transaction);
    }
}