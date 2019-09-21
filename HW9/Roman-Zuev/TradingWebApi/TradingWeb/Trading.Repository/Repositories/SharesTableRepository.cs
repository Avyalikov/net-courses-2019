namespace Trading.Repository.Repositories
{
    using Trading.Core.Models;
    using Trading.Core.Repositories;
    using Trading.Repository.Context;
    using System.Linq;
    using System.Data.Entity;

    public class SharesTableRepository : ISharesTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public SharesTableRepository(TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public SharesEntity this[int i] => this.dbContext.Shares.ToList()[i];

        public int Count => this.dbContext.Shares.ToList().Count;

        public void Add(SharesEntity sharesToAdd)
        {
            this.dbContext.Shares.Add(sharesToAdd);
        }

        public bool Contains(SharesEntity shares)
        {
            return this.dbContext.Shares.Any(s => s.SharesType == shares.SharesType || s.Id == shares.Id);
        }

        public bool ContainsById(int sharesId)
        {
            return this.dbContext.Shares.Any(c => c.Id == sharesId);
        }

        public SharesEntity GetById(int sharesId)
        {
            return this.dbContext.Shares.First(c => c.Id == sharesId);
        }

        public void Remove(SharesEntity sharesToRemove)
        {
            this.dbContext.Shares.Remove(sharesToRemove);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Update(SharesEntity sharesToAdd)
        {
            this.dbContext.Entry(sharesToAdd).State = EntityState.Modified;
        }
    }
}