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
        private readonly string url;
        private readonly string filename;
        private readonly LinksContext context;
        private readonly ILinkService linkService;

        public Downloader(string starturl, string filename, LinksContext context, ILinkService linkService)
        {
            this.url = starturl;// 
            this.filename = filename;
            this.context = context;
            this.linkService = linkService;
        }
          
        public void DownloadHtml(string url, string filename)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, filename);
            }
        }

       

        public List<string> GetLinksFromHtml(string html, string url)
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


        public void Run()
        {
            int iterations = 1;
            for (int i = 0; i < iterations; i++)
            {
                this.DownloadHtml(url, filename);
                List<string> links = this.GetLinksFromHtml(filename, url);


                foreach (string s in links)
                {

                    LinkDTO link = new LinkDTO()
                    {
                        Link = s,
                        IterationId = i + 1
                    };
                    this.linkService.AddLinkToDB(link);

                }

            }
        }

    }
}
