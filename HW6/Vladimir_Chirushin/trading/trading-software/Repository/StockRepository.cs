namespace trading_software
{
    using System.Collections.Generic;
    using System.Linq;

    public class StockRepository : IStockRepository
    {
        public bool Add(Stock stock)
        {
            using (var db = new TradingContext())
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.OrderBy(s => s.StockType).AsEnumerable<Stock>().ToList();
            }
        }

        public int GetNumberOfStocks()
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Count();
            }
        }

        public int GetStockID(string StockType)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockType == StockType).FirstOrDefault().StockID;
            }
        }

        public decimal GetStockPrice(int StockID)
        {
            throw new System.NotImplementedException();
        }

        public string GetStockType(int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(s => s.StockID == StockID).FirstOrDefault().StockType;
            }
        }

        public bool IsStockExist(int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(s => s.StockID == StockID).FirstOrDefault() != null;
            }
        }

        public bool IsStockExist(string StockType)
        {
            using (var db = new TradingContext())
            {
                return db.Stocks.Where(c => c.StockType == StockType).FirstOrDefault() != null;
            }
        }
    }
}