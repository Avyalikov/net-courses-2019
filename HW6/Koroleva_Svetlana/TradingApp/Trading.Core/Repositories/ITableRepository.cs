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
        bool ContainsByID(int entityId);
        bool ContainsByCompositeID(int entityId1, int entityId2);
        Object GetEntityByID(int entityId);
        Object GetEntityByCompositeID(int entityId1, int entityId2);
        IEnumerable<Object> FindEntitiesByRequest(string arguments);
        void SaveChanges();
    }
}
