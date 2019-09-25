namespace TradingApp.View.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class ShareTableRepository : IShareTableRepository
    {
        private readonly TradingAppDb db;

        public ShareTableRepository(TradingAppDb db)
        {
            this.db = db;
        }

        public void Add(ShareEntity shareToAdd)
        {
            this.db.Shares.Add(shareToAdd);
        }

        public bool Contains(ShareEntity entity)
        {
            return this.db.Shares.Any(
                s => s.Name == entity.Name &&
                s.CompanyName == entity.CompanyName &&
                s.Price == entity.Price
            );
        }

        public ICollection<ShareEntity> GetAllShares()
        {
            return this.db.Shares.ToList();
        }

        public ShareEntity GetShareById(int id)
        {
            return this.db.Shares.Where(s => s.Id == id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
