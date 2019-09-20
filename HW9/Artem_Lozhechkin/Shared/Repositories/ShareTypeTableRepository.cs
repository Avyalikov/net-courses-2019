namespace TradingApp.Shared.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class ShareTypeTableRepository : IRepository<ShareTypeEntity>
    {
        private readonly TradingAppDbContext dbContext;

        public ShareTypeTableRepository(TradingAppDbContext tradingAppDbContext)
        {
            dbContext = tradingAppDbContext;
        }
        public int Add(ShareTypeEntity entity)
        {
            this.dbContext.ShareTypes.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<ShareTypeEntity> entities)
        {
            this.dbContext.ShareTypes.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(ShareTypeEntity entity)
        {
            return this.dbContext.ShareTypes.Any(st => st.Name == entity.Name && st.Multiplier == entity.Multiplier);
        }

        public int Delete(ShareTypeEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<ShareTypeEntity> GetAll()
        {
            return this.dbContext.ShareTypes.ToList();
        }

        public ShareTypeEntity GetById(int? id)
        {
            return this.dbContext.ShareTypes.Find(id);
        }

        public IEnumerable<ShareTypeEntity> GetByPredicate(Expression<Func<ShareTypeEntity, bool>> predicate)
        {
            return this.dbContext.ShareTypes.Where(predicate);
        }

        public int Save(ShareTypeEntity entity)
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
