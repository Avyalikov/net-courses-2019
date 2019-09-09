namespace TradingSimulator.Core.Repositories
{
    using System.Collections.Generic;
    using TradingSimulator.Core.Dto;
    public interface ITransactionRepository : IRepository
    {
        List<Transaction> GetTransactions();
        List<Transaction> GetTransactions(int traderId, int top);
        void Push(Transaction transaction);
    }
}