namespace TradingApp.ConsoleTradingManager.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using System;
    using System.Linq.Expressions;

    public class TraderTableRepository : IRepository<TraderEntity>
    {
        private readonly TradingAppDbContext dbContext;
        public TraderTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(TraderEntity entity)
        {
            this.dbContext.Traders.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<TraderEntity> entities)
        {
            this.dbContext.Traders.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(TraderEntity entity)
        {
            return this.dbContext.Traders.Any(t => t.FirstName == entity.FirstName 
                && t.LastName == entity.LastName
                && t.PhoneNumber == entity.PhoneNumber
                && t.Balance == entity.Balance);
        }

        public int Delete(TraderEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<TraderEntity> GetAll()
        {
            return this.dbContext.Traders.ToList();
        }

        public TraderEntity GetById(int? id)
        {
            return this.dbContext.Traders.Find(id);
        }

        public IEnumerable<TraderEntity> GetByPredicate(Expression<Func<TraderEntity, bool>> predicate)
        {
            return this.dbContext.Traders.Where(predicate);
        }

        public int Save(TraderEntity entity)
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
