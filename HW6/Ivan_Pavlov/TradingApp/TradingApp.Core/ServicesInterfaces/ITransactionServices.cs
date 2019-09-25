namespace TradingApp.Core.ServicesInterfaces
{
    using TradingApp.Core.Dto;

    public interface ITransactionServices
    {
        int AddNewTransaction(TransactionStoryInfo args);
    }
}