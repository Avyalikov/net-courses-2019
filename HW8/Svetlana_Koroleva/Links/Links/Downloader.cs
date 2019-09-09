// <copyright file="Downloader.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Links
{
    using HtmlAgilityPack;
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

        private readonly LinksContext context;
        private readonly ILinkService linkService;
        readonly Object locker = new Object();

        public Downloader(LinksContext context, ILinkService linkService)
        {
            this.context = context;
            this.linkService = linkService;
        }

        private void DownloadHtml(string url, string filename)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, filename);
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
            lock (locker)
            {
                int dbiteration;
                List<int> iterations = this.linkService.GetIterations().ToList();
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

        

        //Take(2)- for debug mode
        private async Task AddLinksToDB(string url)
        {
            await Task.Run(() =>
            {
                lock (locker)
                {
                   
                    int iteration = this.GetCurrentIteration();
                    string filename = "html" + iteration + ".html";
                    this.DownloadHtml(url, filename);
                    List<string> links = this.GetLinksFromHtml(filename, url).Take(2).ToList();
                    foreach (string s in links)
                    {
                        LinkDTO link = new LinkDTO()
                        {
                            Link = s,
                            IterationId = iteration
                        };
                        this.linkService.AddLinkToDB(link);
                    }
                }
            }
            );
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

                    Task t = Task.Run(() => AddLinksToDB(url));
                    t.Wait();

                }
                else
                {


                    Task addLinksToDB = Task.Run(() => AddLinksToDB(url));
                    addLinksToDB.Wait();
                    var getlinks = Task.Run(() => this.linkService.GetAllLinksByIteration(this.GetCurrentIteration() - 1).Select(l => l.Url).ToList());
                    getlinks.Wait();
                    List<string> links = getlinks.Result;
                    Parallel.ForEach<string>(links, (link) =>
                    {
                        Task t = Task.Run(() => Run(deep - 1, link));
                        t.Wait();
                    });

                }
            });

            }
        }
    }

