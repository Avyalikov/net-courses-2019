// <copyright file="DownloadService.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace UrlLinksCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore.IService;

    /// <summary>
    /// DownloadService description
    /// </summary>
    public class DownloadService:IDownloadService
    {
        public void DownloadHtml(string url, string filename)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, filename);
                }

                catch (WebException ex)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        return;
                    }
                }
            }
        }

    }
}
