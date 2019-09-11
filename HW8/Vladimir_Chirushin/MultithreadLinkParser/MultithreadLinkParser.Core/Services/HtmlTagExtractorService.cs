using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadLinkParser.Core.Services
{
    public class HtmlTagExtractorService : IHtmlTagExtractorService
    {
        public readonly IPageDownloaderService pageDownloadService;
        public readonly ITagsDataBaseManager tagsDataBaseManager;

        public List<string> parsedLinks;

        public HtmlTagExtractorService(IPageDownloaderService pageDownloadService, ITagsDataBaseManager tagsDataBaseManager)
        {
            this.pageDownloadService = pageDownloadService;
            this.tagsDataBaseManager = tagsDataBaseManager;
        }


        public List<string> ExtractTags(string rawHttpData, string urlToParse)
        {
            HashSet<string> urlHashSet = new HashSet<string>();
            var doc = new HtmlDocument();
            doc.LoadHtml(rawHttpData);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes != null)
            {
                foreach (var n in nodes)
                {
                    string href = n.Attributes["href"].Value;
                    var uri = new Uri(href, UriKind.RelativeOrAbsolute);

                    if (!uri.IsAbsoluteUri)
                    {
                        uri = new Uri(new Uri(urlToParse), uri);
                    }

                    if (uri.Host == new Uri(urlToParse).Host)
                    {
                        urlHashSet.Add(uri.ToString());
                    }
                }
            }

            return urlHashSet.ToList();
        }
    }
}
