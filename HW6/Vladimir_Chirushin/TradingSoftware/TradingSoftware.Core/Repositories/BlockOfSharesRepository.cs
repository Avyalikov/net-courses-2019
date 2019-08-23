using System;
using System.Collections.Generic;
using TradingSoftware.Core.Models;

namespace TradingSoftware.Core.Repositories
{
    public class BlockOfSharesRepository : IBlockOfSharesRepository
    {
        public void Insert(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                db.BlockOfSharesTable.Add(blockOfShares);
                db.SaveChanges();
            }
        }
        public bool IsClientHasStockType(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault() != null;
            }
        }
        public int GetClientStockAmount(int ClientID, int StockID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.StockID == StockID && b.ClientID == ClientID).FirstOrDefault().Amount;
            }
        }
        public void ChangeShareAmountForClient(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClientID == blockOfShares.ClientID &&
                                 b.StockID == blockOfShares.StockID))
                    .FirstOrDefault();
                entry.Amount += blockOfShares.Amount;
                db.SaveChanges();
            }
        }

        public  IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.OrderBy(b => b.ClientID).AsEnumerable<BlockOfShares>().ToList();
            }
        }
    }
}
