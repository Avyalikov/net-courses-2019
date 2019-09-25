namespace TradingApp.Shared.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class ShareTableRepository : IRepository<ShareEntity>
    {
        private readonly TradingAppDbContext dbContext;
        public ShareTableRepository(TradingAppDbContext tradingAppDbContext)
        {
            this.dbContext = tradingAppDbContext;
        }
        public int Add(ShareEntity entity)
        {
            this.dbContext.Shares.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<ShareEntity> entities)
        {
            this.dbContext.Shares.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(ShareEntity entity)
        {
            return this.dbContext.Shares.Any(s => s.Owner == entity.Owner
                && s.Amount == entity.Amount
                && s.Stock == entity.Stock
                && s.ShareType == entity.ShareType);
        }

        public int Delete(ShareEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<ShareEntity> GetAll()
        {
            return this.dbContext.Shares.ToList();
        }

        public ShareEntity GetById(int? id)
        {
            return this.dbContext.Shares.Find(id);
        }

        public IEnumerable<ShareEntity> GetByPredicate(Expression<Func<ShareEntity, bool>> predicate)
        {
            return this.dbContext.Shares.Where(predicate);
        }

        public int Save(ShareEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
