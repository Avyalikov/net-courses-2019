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
    public interface IOnePKTableRepository:ITableRepository
    {
        bool ContainsByID(int entityId);
        Object GetEntityByID(int entityId);
    }
}
