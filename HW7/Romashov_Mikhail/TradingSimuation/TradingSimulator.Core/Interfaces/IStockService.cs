using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Interfaces
{
    public interface IStockService
    {
        StockEntity GetStockById(int stockID);
        StockEntity GetStockByName(string stockName);
        bool ContainsByName(string stockName);
    }
}