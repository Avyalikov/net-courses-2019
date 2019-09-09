// <copyright file="IUnitOfWork.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.Repository;
   

    /// <summary>
    /// IUnitOfWork description
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        ILinkRepository Links{get;}
        void Save();
    }
}
