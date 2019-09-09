namespace TradingApp.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        int Add(T entity);
        int AddRange(IList<T> entities);
        int Save(T entity);
        int Delete(T entity);
        T GetById(int? id);
        IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        bool Contains(T entity);
        int SaveChanges();
    }
}
