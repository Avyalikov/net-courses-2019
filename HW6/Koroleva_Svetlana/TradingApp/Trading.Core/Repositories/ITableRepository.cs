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
    public interface ITableRepository
    {
        void Add(Object entity);
        void AddRange(IEnumerable<Object> entities);
        bool ContainsByPK(params object[] pk);
        IEnumerable<Object> FindEntitiesByRequestDTO(object DTOarguments);
        IEnumerable<Object> FindEntitiesByRequest(params object[] arguments);
        void SaveChanges();
        Object Find(params object[] key);
        bool Contains(Object entity);
        int Count();
        Object GetElementAt(int position);
        Object Single();
        Object OrderById(int type);
        Object First();
        IEnumerable<Object> Where(params object[] arguments);
    }
}
