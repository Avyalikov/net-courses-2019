namespace trading_software
{
    public interface IStockManager
    {
        void AddNewStock();
        void AddStock(string stockName, decimal stockPrice);
        void AddStock(Stock stock);
        Stock SelectRandom();
        void ReadAllStocks();
    }
}