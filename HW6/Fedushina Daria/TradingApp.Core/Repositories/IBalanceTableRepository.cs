using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IBalanceTableRepository
    {
        void Change(BalanceEntity entity);

        decimal GetBalance (string balanceId);

        List<BalanceEntity> GetAll(int userId);

        BalanceEntity Get(string balanceId);

        void SaveChanges();

        bool Contains(string balanceId);

        bool Contains(BalanceEntity balanceEntity);

        void Add(BalanceEntity balanceEntity);
    }
}