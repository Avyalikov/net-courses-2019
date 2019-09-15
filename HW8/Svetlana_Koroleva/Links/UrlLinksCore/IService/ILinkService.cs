// <copyright file="ILinkService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.IService
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
        void AddParsedLinksToDB(List<string> links, int iteration);
        bool ContainsLink(string link);
        IEnumerable<Link> GetAllLinks();
        IEnumerable<String> GetAllLinksByIteration(int iterationId);
        IEnumerable<int> GetIterations();
        int GetCurrentIteration();
    }
}
