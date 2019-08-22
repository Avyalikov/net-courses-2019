using System.Collections.Generic;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITraderStockTableRepository
    {
        void Add(StockToTraderEntity entityToAdd);
        void SaveChanges();
        bool Contains(StockToTraderEntity stockToTraderEntity);
        StockToTraderEntity GetStocksFromSeller(BuyArguments buyArguments);
        void SubtractStockFromSeller(BuyArguments args);
        void AdditionalStockToCustomer(BuyArguments args);
        bool ContainsSeller(BuyArguments args);
        bool ContainsCustomer(BuyArguments args);
        List<int> GetList();
        bool ContainsById(int id);
        StockToTraderEntity GetTraderStockById(int id);
    }
}
