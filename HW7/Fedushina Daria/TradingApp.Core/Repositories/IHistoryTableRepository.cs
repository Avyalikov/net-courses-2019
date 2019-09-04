using System;
using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IHistoryTableRepository
    {
        bool Contains(int transactionId);
        int Add(TransactionHistoryEntity entity);  //should return ID
        void SaveChanges();
        TransactionHistoryEntity Get(int transactionId);
        List<TransactionHistoryEntity> Get(DateTime dateTime);
        List<TransactionHistoryEntity> GetAllFromBase();
        List<TransactionHistoryEntity> GetAll(int userId);
        int GetId(TransactionInfo transactionInfo);
    }
}