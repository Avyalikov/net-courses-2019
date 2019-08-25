namespace TradingSimulator.Core.Interfaces
{
    using Dto;

    public interface ITransactionService
    {
        Transaction MakeDeal(int sellerId, int buyerId, string shareName, int quantity);
        void Save(Transaction transaction);
    }
}