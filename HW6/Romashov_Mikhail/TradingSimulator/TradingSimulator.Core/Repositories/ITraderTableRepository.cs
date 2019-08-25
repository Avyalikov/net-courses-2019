using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITraderTableRepository
    {
        void Add(TraderEntityDB entity);
        void SaveChanges();
        bool Contains(TraderEntityDB entityToAdd);
        bool ContainsById(int entityId);
        TraderEntityDB GetById(int traderID);
        void SubstractBalance(int traderID, decimal amount);
        void AdditionBalance(int traderID, decimal amount);
        bool ContainsByName(string traderName);
        TraderEntityDB GetByName(string traderName);
        List<int> GetListTradersId();
    }
}
