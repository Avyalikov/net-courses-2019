namespace TradingApp.Shared.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class StockTableRepository : IRepository<StockEntity>
    {
        private readonly TradingAppDbContext dbContext;

        public StockTableRepository(TradingAppDbContext tradingAppDbContext)
        {
            dbContext = tradingAppDbContext;
        }

        public int Add(StockEntity entity)
        {
            this.dbContext.Stocks.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<StockEntity> entities)
        {
            this.dbContext.Stocks.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(StockEntity entity)
        {
            return this.dbContext.Stocks.Any(st => st.PricePerUnit == entity.PricePerUnit && st.Company == entity.Company);
        }

        public int Delete(StockEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<StockEntity> GetAll()
        {
            return this.dbContext.Stocks.ToList();
        }

        public StockEntity GetById(int? id)
        {
            return this.dbContext.Stocks.Find(id);
        }

        public IEnumerable<StockEntity> GetByPredicate(Expression<Func<StockEntity, bool>> predicate)
        {
            return this.dbContext.Stocks.Where(predicate);
        }

        public int Save(StockEntity entity)
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
