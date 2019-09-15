// <copyright file="IParser.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.IService
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IParser description
    /// </summary>
    public interface IParserService
    {
        List<string> GetLinksFromHtml(string html, string url);
    }
}
