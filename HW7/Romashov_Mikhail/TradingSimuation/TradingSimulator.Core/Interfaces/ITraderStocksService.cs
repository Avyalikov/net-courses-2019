using System.Collections.Generic;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Interfaces
{
    public interface ITraderStocksService
    {
        int AddNewStockToTrader(TraderInfo trader, StockInfo stock);
        List<int> GetListTradersStock();
        IEnumerable<string> GetTradersStockById(int traderId);
        StockToTraderEntityDB GetTraderStockById(int id);

        int GetCountIds();


    }
}