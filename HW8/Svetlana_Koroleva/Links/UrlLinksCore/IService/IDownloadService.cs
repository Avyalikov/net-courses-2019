// <copyright file="IDownloadSevice.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.IService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IDownloadSevice description
    /// </summary>
    public interface IDownloadService
    {
        void DownloadHtml(string url, string filename);
    }
}
