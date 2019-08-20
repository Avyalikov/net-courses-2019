namespace trading_software
{
    using System.Collections.Generic;
    using System.Linq;

    public class BlockOfSharesRepository : IBlockOfSharesRepository
    {
        public void Add(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClientID == blockOfShares.ClientID &&
                                 b.StockID == blockOfShares.StockID))
                    .FirstOrDefault();
                if (entry == null)
                {
                    db.BlockOfSharesTable.Add(blockOfShares);
                    db.SaveChanges();
                }
                else
                {
                    entry.Amount += blockOfShares.Amount;
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.OrderBy(b => b.ClientID).AsEnumerable<BlockOfShares>().ToList();
            }
        }

        public int GetClientStockAmount(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault().Amount;
            }
        }

        public bool IsClientHasStockType(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault() != null;
            }
        }
    }
}