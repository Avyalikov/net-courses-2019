using System.Collections.Generic;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Interfaces
{
    public interface ITraderService
    {
        int RegisterNewTrader(TraderInfo trader);
        TraderEntityDB GetTraderById(int traderID);
        TraderEntityDB GetTraderByName(string traderName);
        bool ContainsByName(string traderName);
        List<int> GetList();
        List<string> GetListOfTraders(int maxCount);
        decimal GetBalanceById(int traderId);
        int GetCountIds();
    }
}
