namespace TradeSimulator.Core.Repositories
{
    using System.Collections.Generic;
    using TradeSimulator.Core.Models;

    public interface IStockOfClientTableRepository
    {
        void Add(StockOfClientEntity entity);
        void Delete(StockOfClientEntity entity);
        void SaveChanges();

        StockOfClientEntity GetStockOfClientEntityByClientIdAndType(int clientId, string typeOfStock);
        ICollection<StockOfClientEntity> GetFullStockOfClientByClientId(int clientId);
        StockOfClientEntity GetStockOfClientEntityByAccountIdAndType(int accountId, string typeOfStock);
        ICollection<StockOfClientEntity> GetFullStockOfClientByAccountId(int accountId);
        ICollection<StockOfClientEntity> GetAllStockOfClient();
    }
}
