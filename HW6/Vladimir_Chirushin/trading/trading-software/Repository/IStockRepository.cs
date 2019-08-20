namespace trading_software
{
    using System.Collections.Generic;

    public interface IStockRepository
    {
        bool Add(Stock stock);
        string GetStockType(int StockID);
        int GetStockID(string StockType);
        int GetNumberOfStocks();
        decimal GetStockPrice(int StockID);
        IEnumerable<Stock> GetAllStocks();
        bool IsStockExist(int StockID);
        bool IsStockExist(string StockType);
    }
}