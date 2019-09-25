using System.Collections.Generic;

namespace TradingSimulator.Core.Interfaces
{
    public interface IBankruptService
    {
        List<string> GetListTradersFromOrangeZone();
        List<string> GetListTradersFromBlackZone();

    }
}