namespace WebApiTradingServer.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

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

        public bool IsClientHasShareType(int clientID, int shareID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.ShareID == shareID && b.ClientID == clientID).FirstOrDefault() != null;
            }
        }

        public int GetClientShareAmount(int clientID, int shareID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.ShareID == shareID && b.ClientID == clientID).FirstOrDefault().Amount;
            }
        }

        public void ChangeShareAmountForClient(BlockOfShares blockOfShares)
        {
            using (var db = new TradingContext())
            {
                var entry = db.BlockOfSharesTable
                    .Where(b => (b.ClientID == blockOfShares.ClientID &&
                                 b.ShareID == blockOfShares.ShareID))
                    .FirstOrDefault();
                entry.Amount += blockOfShares.Amount;
                db.SaveChanges();
            }
        }

        public IEnumerable<BlockOfShares> GetAllBlockOfShares()
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.OrderBy(b => b.ClientID).AsEnumerable<BlockOfShares>().ToList();
            }
        }

        public IEnumerable<BlockOfShares> GetClientShares(int clientID)
        {
            using (var db = new TradingContext())
            {
                return db.BlockOfSharesTable.Where(b => b.ClientID == clientID).AsEnumerable<BlockOfShares>().ToList();
            }
        }

        public void Remove(BlockOfShares blockOfShare)
        {
            using (var db = new TradingContext())
            {
                var blockToDelete = db.BlockOfSharesTable
                    .Where(b => b.ShareID == blockOfShare.ShareID && 
                           b.ClientID == blockOfShare.ClientID)
                    .FirstOrDefault();
                db.BlockOfSharesTable.Remove(blockToDelete);
                db.SaveChanges();
            }
        }
    }
}
