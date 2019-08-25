namespace TradingSoftware.ConsoleClient.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class SharesRepository : ISharesRepository
    {
        public bool Insert(Share share)
        {
            using (var db = new TradingContext())
            {
                db.Shares.Add(share);
                db.SaveChanges();
                return true;
            }
        }

        public string GetShareType(int shareID)
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Where(s => s.ShareID == shareID).FirstOrDefault().ShareType;
            }
        }

        public int GetShareID(string shareType)
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Where(c => c.ShareType == shareType).FirstOrDefault().ShareID;
            }
        }

        public int GetNumberOfShares()
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Count();
            }
        }

        public decimal GetSharePrice(int shareID)
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Where(s => s.ShareID == shareID).FirstOrDefault().Price;
            }
        }

        public IEnumerable<Share> GetAllShares()
        {
            using (var db = new TradingContext())
            {
                return db.Shares.OrderBy(s => s.ShareType).AsEnumerable<Share>().ToList();
            }
        }

        public bool IsShareExist(int shareID)
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Where(s => s.ShareID == shareID).FirstOrDefault() != null;
            }
        }

        public bool IsShareExist(string shareType)
        {
            using (var db = new TradingContext())
            {
                return db.Shares.Where(c => c.ShareType == shareType).FirstOrDefault() != null;
            }
        }

        public void ChangeSharePrice(int shareID, decimal price)
        {
            using (var db = new TradingContext())
            {
                var entry = db.Shares
                       .Where(s => s.ShareID == shareID)
                       .FirstOrDefault();
                entry.Price = price;
                db.SaveChanges();
            }
        }
    }
}