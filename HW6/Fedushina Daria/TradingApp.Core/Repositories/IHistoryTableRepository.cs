using System;
using System.Collections.Generic;
using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IHistoryTableRepository
    {
        bool Contains(string transactionId);
        string Add(TransactionHistoryEntity entity);  //should return ID
        void SaveChanges();
        TransactionHistoryEntity Get(string transactionId);
        List<TransactionHistoryEntity> Get(DateTime dateTime);
        List<TransactionHistoryEntity> GetAll(string userId);
    }
}