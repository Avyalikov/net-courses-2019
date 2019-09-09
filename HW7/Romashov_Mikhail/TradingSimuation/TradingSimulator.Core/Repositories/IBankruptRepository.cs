using System.Collections.Generic;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IBankruptRepository
    {
        IEnumerable<TraderEntityDB> GetTradersWithZeroBalance();
        IEnumerable<TraderEntityDB> GetTradersWithNegativeBalance();
    }
}
