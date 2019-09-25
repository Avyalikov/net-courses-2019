namespace TradingApp.ConsoleTradingManager.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class TransactionTableRepository : IRepository<TransactionEntity>
    {
        private readonly TradingAppDbContext dbContext;
        public TransactionTableRepository(TradingAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(TransactionEntity entity)
        {
            this.dbContext.Transactions.Add(entity);
            this.SaveChanges();
            return entity.Id;
        }

        public int AddRange(IList<TransactionEntity> entities)
        {
            this.dbContext.Transactions.AddRange(entities);
            return SaveChanges();
        }

        public bool Contains(TransactionEntity entity)
        {
            return this.dbContext.Transactions.Any(tr => tr.Buyer == entity.Buyer
                && tr.Seller == entity.Seller
                && tr.Share == entity.Share
                && tr.TransactionPayment == entity.TransactionPayment
                && tr.TransactionDate == entity.TransactionDate);
        }

        public int Delete(TransactionEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        public List<TransactionEntity> GetAll()
        {
            return this.dbContext.Transactions.ToList();
        }

        public TransactionEntity GetById(int? id)
        {
            return this.dbContext.Transactions.Find(id);
        }

        public IEnumerable<TransactionEntity> GetByPredicate(Expression<Func<TransactionEntity, bool>> predicate)
        {
            return this.dbContext.Transactions.Where(predicate);
        }

        public int Save(TransactionEntity entity)
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
