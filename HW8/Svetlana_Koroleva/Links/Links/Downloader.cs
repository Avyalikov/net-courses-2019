// <copyright file="Downloader.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Links
{
    using HtmlAgilityPack;
    using LinkContext;
    using LinkContext.DAL;
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
    using UrlLinksCore.Service;

    /// <summary>
    /// Downloader description
    /// </summary>
    public class Downloader
    {
        readonly Object locker = new Object();
        private void DownloadHtml(string url, string filename)
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

        private List<string> GetLinksFromHtml(string html, string url)
        {
            Uri uri = new Uri(url);
            string protocol_domen = url.Replace(uri.LocalPath, string.Empty);
            List<string> links = new List<string>();
            HtmlDocument document = new HtmlDocument();
            document.Load(html);
            HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes("//a");
            if (htmlNodes != null)
                foreach (HtmlNode node in htmlNodes)
                {
                    var atribute = node.GetAttributeValue("href", null);
                    if (!(atribute is null))
                    {
                        if (atribute.StartsWith(protocol_domen) && !atribute.Contains("#") && !atribute.Contains("index"))
                        {
                            if (!links.Contains(atribute))
                            {
                                links.Add(atribute.ToString());
                            }
                        }
                        if (atribute.StartsWith("/wiki/") && !atribute.Contains("#") && !atribute.Contains(":"))
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            string linkToAdd = stringBuilder.Append(protocol_domen).Append(atribute).ToString();
                            if (!links.Contains(linkToAdd))
                            {
                                links.Add(linkToAdd);
                            }
                        }
                    }
                }

            return links;
        }


        private int GetCurrentIteration()
        {
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    LinkService linkService = new LinkService(unitOfWork);
                    int dbiteration;
                    List<int> iterations = linkService.GetIterations().ToList();
                    if (iterations.Count() == 0)
                    {
                        dbiteration = 1;
                    }
                    else
                    {
                        dbiteration = iterations.Last() + 1;
                    }
                    return dbiteration;
                }
            }
        }


        //Take(5)- for debug mode
        private void AddLinksToDB(string url)
        {
            lock (locker)
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    LinkService linkService = new LinkService(unitOfWork);
                    int iteration = this.GetCurrentIteration();
                    string filename = "html" + iteration + ".html";
                    this.DownloadHtml(url, filename);
                    List<string> links = this.GetLinksFromHtml(filename, url).Take(5).ToList();
                    foreach (string s in links)
                    {
                        LinkDTO link = new LinkDTO()
                        {
                            Link = s,
                            IterationId = iteration
                        };
                        linkService.AddLinkToDB(link);
                    }
                }
            }
        }


        public async Task Run(int deep, string url)
        {
            await Task.Run(() =>
             {
                 Thread.Sleep(500);

                 if (deep <= 0)
                 {
                     return;
                 }
                 if (deep == 1)
                 {
                     AddLinksToDB(url);
                 }
                 else
                 {
                     using (UnitOfWork unitOfWork = new UnitOfWork())
                     {
                         LinkService linkService = new LinkService(unitOfWork);
                         AddLinksToDB(url);
                         int previousIteration = this.GetCurrentIteration() - 1;
                         var links = linkService.GetAllLinksByIteration(previousIteration);
                         if (links.Count() != 0)
                         {
                             Parallel.ForEach<string>(links, (link) =>
                             {
                                 Task t = Run(deep - 1, link);
                                 t.Wait();
                             });
                         }
                     }
                 }
             });
        }
    }
}