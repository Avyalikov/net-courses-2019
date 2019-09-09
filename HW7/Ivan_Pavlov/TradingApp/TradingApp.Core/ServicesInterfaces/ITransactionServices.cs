namespace TradingApp.Core.ServicesInterfaces
{
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;

    public interface ITransactionServices
    {
        int AddNewTransaction(TransactionStoryInfo args);
        List<TransactionStoryEntity> GetUsersTransaction(int userId);
        void AddShareInPortfolio(TransactionStoryInfo args);
    }
}