namespace trading_software
{
    public interface IStockManager
    {
        void ManualAddStock();

        void AddStock(string stockName, decimal stockPrice);

        void AddStock(Stock stock);

        int SelectRandomID();

        void ReadAllStocks();
    }
}