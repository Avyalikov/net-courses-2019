// <copyright file="Downloader.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Links
{
    using HtmlAgilityPack;
    using LinkDBContext;
    using LinkDBContext.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using UrlLinksCore.DTO;
    using UrlLinksCore.Model;
    using UrlLinksCore.IService;
    using UrlLinksCore.Services;

    /// <summary>
    /// Downloader description
    /// </summary>
    public class DownloadWorker
    {
        private readonly IDownloadService downloadService;
        private readonly IParserService parserService;

        public DownloadWorker(IDownloadService downloadService, IParserService parserService)
        {
            this.downloadService = downloadService;
            this.parserService = parserService;
        }


        private void SingleIteration(string url, LinkService linkService)
        {

            string filename = url.GetHashCode() + ".html";
            this.downloadService.DownloadHtml(url, filename);
            var linksToAdd = this.parserService.GetLinksFromHtml(filename, url).ToList();
            int iteration = linkService.GetCurrentIteration();
            if (linksToAdd != null)
            {
                linkService.AddParsedLinksToDB(linksToAdd, iteration);
            }
        }


        public async Task RunRecursively(int deep, string url)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                LinkService linkService = new LinkService(unitOfWork);
                await Task.Run(() =>
             {
                 Thread.Sleep(500);

                 if (deep <= 0)
                 {
                     return;
                 }
                 if (deep == 1)
                 {
                     this.SingleIteration(url, linkService);
                 }
                 else
                 {
                     this.SingleIteration(url, linkService);
                     var linksFromPreviousIteration = linkService.GetAllLinksByIteration(linkService.GetCurrentIteration() - 1).ToList();
                     if (linksFromPreviousIteration.Count() != 0)
                     {
                         Parallel.ForEach<string>(linksFromPreviousIteration, (link) =>
                             {
                                 Task t = RunRecursively(deep - 1, link);
                                 t.Wait();
                             });
                     }
                 }
             });
            }
        }
    }
}