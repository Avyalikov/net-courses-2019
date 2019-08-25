using System.Collections.Generic;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IBankruptRepository
    {
        List<TraderEntityDB> GetTradersWithZeroBalance();

        List<TraderEntityDB> GetTradersWithNegativeBalance();
    }
}
