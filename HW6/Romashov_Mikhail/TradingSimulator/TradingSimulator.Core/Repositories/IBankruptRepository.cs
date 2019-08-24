using System.Collections.Generic;

namespace TradingSimulator.Core.Repositories
{
    public interface IBankruptRepository
    {
        List<string> GetTradersWithZeroBalance();

        List<string> GetTradersWithNegativeBalance();
    }
}
