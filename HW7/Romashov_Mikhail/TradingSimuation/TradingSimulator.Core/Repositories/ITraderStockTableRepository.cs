using System.Collections.Generic;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITraderStockTableRepository
    {
        void Add(StockToTraderEntityDB entityToAdd);
        void SaveChanges();
        bool Contains(StockToTraderEntityDB stockToTraderEntity);
        StockToTraderEntityDB GetStocksFromSeller(BuyArguments buyArguments);
        void SubtractStockFromSeller(BuyArguments args);
        void AdditionalStockToCustomer(BuyArguments args);
        bool ContainsSeller(BuyArguments args);
        bool ContainsCustomer(BuyArguments args);
        List<int> GetListOfTraderStocksIds();
        bool ContainsById(int id);
        IEnumerable<StockToTraderEntityDB> GetTradersStockById(int traderId);
        StockToTraderEntityDB GetTraderStockById(int id);
        int GetCountOfListOfTraderStocksIds();
    }
}
