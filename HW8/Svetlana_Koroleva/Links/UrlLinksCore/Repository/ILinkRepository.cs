// <copyright file="ILinkRepository.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.Model;

    /// <summary>
    /// ILinkRepository description
    /// </summary>
    public interface ILinkRepository
    {
        void Add(Link link);
        IEnumerable<Link> GetAll();
        IEnumerable<Link> GetByCondition(Func<Link, bool> predicate);
        
    }
}
