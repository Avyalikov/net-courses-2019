namespace TradingSimulator.DataBase.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Dto;
    using Core.Repositories;
    using Model;

    public class ShareRepository : IShareRepository
    {
        private readonly TradingDbContext db;

        public ShareRepository(TradingDbContext db) => this.db = db;

        public int GetSharesCount(int OwnerId) =>
            this.db.Shares
            .Where(s => s.Owner.ID == OwnerId)
            .Count();

        public List<Share> GetShareList(int OwnerId)
        {
            var sharesEntity = this.db.Shares
                .Where(s => s.Owner.ID == OwnerId)
                .Select(s => s);

            List<Share> temp = new List<Share>();

            foreach (var share in sharesEntity)
            {
                temp.Add(
                    new Share()
                    {
                        id = share.ID,
                        name = share.Name,
                        price = share.Price,
                        quantity = share.Quantity,
                        ownerId = OwnerId,
                    });
            }

            return temp;
        }

        public Share GetShare(int ShareId)
        {
            var shareEntity = this.db.Shares
                .Where(s => s.ID == ShareId)
                .SingleOrDefault();

            if (shareEntity == null)
            {
                return null;
            }

            return new Share()
            {
                id = shareEntity.ID,
                name = shareEntity.Name,
                price = shareEntity.Price,
                quantity = shareEntity.Quantity,
                ownerId = shareEntity.Owner.ID,
            };
        }

        public Share GetShare(int OwnerId, string shareName)
        {
            var shareEntity = this.db.Shares
                .Where(s => s.Owner.ID == OwnerId && s.Name == shareName)
                .SingleOrDefault();

            if (shareEntity == null)
            {
                return null;
            }

            return new Share()
            {
                id = shareEntity.ID,
                name = shareEntity.Name,
                price = shareEntity.Price,
                quantity = shareEntity.Quantity,
                ownerId = OwnerId,
            };
        }

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