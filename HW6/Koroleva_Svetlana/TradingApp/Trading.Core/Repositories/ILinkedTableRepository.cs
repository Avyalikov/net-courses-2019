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
    public interface ILinkedTableRepository
    {
        bool ContainsByCompositeID(int entityId1, int entityId2);
        Object GetEntityByCompositeID(int entityId1, int entityId2);
    }
}
