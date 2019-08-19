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
    }
}
