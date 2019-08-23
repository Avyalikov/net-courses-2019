using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSoftware.Core.Repositories
{
    public interface IStockRepository
    {
        bool Insert(Stock stock);
        string GetStockType(int StockID);
        int GetStockID(string StockType);
        int GetNumberOfStocks();
        decimal GetStockPrice(int StockID);
        IEnumerable<Stock> GetAllStocks();
        bool IsStockExist(int StockID);
        bool IsStockExist(string StockType);
        void ChangeStockPrice(int StockID, decimal Price);
    }
}
