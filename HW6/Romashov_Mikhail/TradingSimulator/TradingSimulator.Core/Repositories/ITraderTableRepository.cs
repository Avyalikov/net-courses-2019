using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITraderTableRepository
    {
        void Add(TraderEntity entity);
        void SaveChanges();
        bool Contains(TraderEntity entityToAdd);
        bool ContainsById(int entityId);
        TraderEntity Get(int traderID);
        void SubstractBalance(int traderID, decimal amount);
        void AdditionBalance(int traderID, decimal amount);
        bool ContainsByName(string traderName);
        TraderEntity GetByName(string traderName);
        List<int> GetListTradersId();
    }
}
