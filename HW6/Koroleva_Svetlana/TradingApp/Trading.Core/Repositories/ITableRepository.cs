// <copyright file="ITableRepository.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// ITableRepository description
    /// </summary>
    public interface ITableRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void SaveChanges();
        bool ContainsByPK(params object[] pk);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate );
        IEnumerable<TEntity> FindEntitiesByRequestDTO(object DTOarguments);
        IEnumerable<TEntity> FindEntitiesByRequest(params object[] arguments);
        TEntity FindByPK(params object[] key);
        bool Contains(TEntity entity);
        bool ContainsDTO(Object entity);
        int Count();
        TEntity GetElementAt(int position);
        IEnumerable<TEntity> OrderById(object i);
        TEntity First();
        //TEntity Single(IQueryable<TEntity> entities);
       
    }
}
