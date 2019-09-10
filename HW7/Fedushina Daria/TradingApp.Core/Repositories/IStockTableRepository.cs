using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;

namespace TradingApp.Core.Repositories
{
    public interface IStockTableRepository
    {
        void Add(StockEntity entity);
        void SaveChanges();
        bool Contains(StockEntity entity);
        bool Contains(int entityId);
        StockEntity Get(int stockId);
        List<StockEntity> GetAll();
        int GetId(StockRegistrationInfo stockInfo);
        bool Update(StockEntity stock);
        void Delete(int id);
    }
}