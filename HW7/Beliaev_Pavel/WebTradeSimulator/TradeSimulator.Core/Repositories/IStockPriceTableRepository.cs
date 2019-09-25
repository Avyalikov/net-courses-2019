namespace TradeSimulator.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradeSimulator.Core.Models;

    public interface IStockPriceTableRepository
    {
        void Add(StockPriceEntity entity);
        void Remove(StockPriceEntity entity);
        void SaveChanges();

        StockPriceEntity GetStockPriceEntityByStockType(string typeOfStock);
        ICollection<StockPriceEntity> GetAllStockPrice();
    }
}
