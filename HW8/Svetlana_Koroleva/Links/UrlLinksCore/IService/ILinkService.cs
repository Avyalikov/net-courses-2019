// <copyright file="ILinkService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.DTO;
    using UrlLinksCore.Model;

    /// <summary>
    /// ILinkService description
    /// </summary>
    public interface ILinkService
    {
        void AddLinkToDB(LinkDTO linkDTO);
        bool ContainsLink(string link);
        IEnumerable<Link> GetAllLinks();
        IEnumerable<Link> GetAllLinksByIteration(int iterationId);
        IEnumerable<int> GetIterations();

       
    }
}
