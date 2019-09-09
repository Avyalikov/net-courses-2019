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
        void SaveChanges();
        bool ContainsByPK(params object[] pk);
        bool Contains(TEntity entity);
        bool ContainsDTO(TEntity entity);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate );
        TEntity FindByPK(params object[] key);
        int Count();
        TEntity GetElementAt(int position);
     
       
    }
}
