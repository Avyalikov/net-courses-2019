namespace TradingSimulator.DataBase.Repositories
{
    using Core.Dto;
    using Core.Repositories;
    using Model;
    using System.Collections.Generic;
    using System.Linq;

    public class ShareRepository : IShareRepository
    {
        private readonly TradingDbContext db;
        public ShareRepository(TradingDbContext db) => 
            this.db = db;

        public int GetSharesCount(int OwnerId) =>
            this.db.Shares
            .Where(s => s.Owner.ID == OwnerId)
            .Count();

        public List<Share> GetShareList(int OwnerId) =>
            this.db.Shares
            .Where(s => s.Owner.ID == OwnerId)
            .Select(s => s)
            .ToList()
            .ToShare();

        public Share GetShare(int ShareId) =>
            this.db.Shares
            .Where(s => s.ID == ShareId)
            .SingleOrDefault()
            .ToShare();

        public Share GetShare(int OwnerId, string shareName) =>
            this.db.Shares
            .Where(s => s.Owner.ID == OwnerId && s.Name == shareName)
            .SingleOrDefault()
            .ToShare();

        public void Push(Share share)
        {
            var shareEntity =
                this.db.Shares
                .Where(s => s.ID == share.id)
                .SingleOrDefault();

            if (shareEntity == null)
            {
                this.db.Shares.Add(
                    new ShareEntity
                    {
                        Name = share.name,
                        Price = share.price,
                        Quantity = share.quantity,
                        Owner = this.db.Traders.Where(t => t.ID == share.ownerId).Single(),
                    });

                return;
            }

            shareEntity.Name = share.name;
            shareEntity.Price = share.price;
            shareEntity.Quantity = share.quantity == 0 ? shareEntity.Quantity : share.quantity;
        }

        public void Remove(Share share)
        {
            var shareEntity = db.Shares
                .Where(s => s.ID == share.id)
                .Single();

            db.Shares.Remove(shareEntity);
        }

        public void SaveChanges() => this.db.SaveChanges();
    }
}