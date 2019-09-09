namespace TradingSimulator.Core.Repositories
{
    using System.Collections.Generic;
    using TradingSimulator.Core.Dto;
    public interface ITransactionRepository : IRepository
    {
        List<Transaction> GetTransactions();
        void Push(Transaction transaction);
    }
}