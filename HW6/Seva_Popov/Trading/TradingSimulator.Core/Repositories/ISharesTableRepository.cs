using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ISharesTableRepository
    {
        bool Contains(SharesEntity sharesEntity);
        bool ContainsById(int entityId);
        void Add(SharesEntity sharesEntity);
        void SaveChanges();
        SharesEntity Get(int sharesId);
    }
}
