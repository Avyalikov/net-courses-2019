﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WikiURLCollector.Core.Interfaces;
using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Services
{
    public class UrlParsingService
    {
        private readonly IUrlService urlService;
        public UrlParsingService(IUrlService urlService)
        {
            this.urlService = urlService;
        }
        public IEnumerable<UrlEntity> ExtractAllUrlsFromPage(string rawDocument, int iteration)
        {
            List<UrlEntity> wikiUrls = new List<UrlEntity>();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(rawDocument);
            IEnumerable<HtmlNode> allUrls = document.DocumentNode.SelectNodes("//a[@href]");
            var filtredUrls = allUrls.Where(a => a.Attributes["href"].Value.StartsWith("/wiki/"));
            foreach (var url in filtredUrls)
            {
                UrlEntity urlEntity = new UrlEntity() { URL = url.Attributes["href"].Value, IterationId = iteration };
                wikiUrls.Add(urlEntity);
            }
            if (wikiUrls.Count() > 0)
                return wikiUrls;
            return null;
        }
    }
}
