namespace TradingApp.Shared.Repositories
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System;
    using System.Linq.Expressions;

    public class CompanyTableRepository : IRepository<CompanyEntity>
    {
        private readonly TradingAppDbContext dbContext;
        public CompanyTableRepository(TradingAppDbContext tradingAppDbContext)
        {
            this.dbContext = tradingAppDbContext;
        }

        public int Add(CompanyEntity entity)
        {
            this.dbContext.Companies.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<CompanyEntity> entities)
        {
            this.dbContext.Companies.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(CompanyEntity entity)
        {
            return this.dbContext.Companies.Any(c => c.Id == entity.Id && c.Name == entity.Name);
        }

        public int Delete(CompanyEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<CompanyEntity> GetAll()
        {
            return this.dbContext.Companies.ToList();
        }

        public CompanyEntity GetById(int? id)
        {
            return this.dbContext.Companies.Find(id);
        }

        public IEnumerable<CompanyEntity> GetByPredicate(Expression<Func<CompanyEntity, bool>> predicate)
        {
            return this.dbContext.Companies.Where(predicate);
        }

        public int Save(CompanyEntity entity)
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
