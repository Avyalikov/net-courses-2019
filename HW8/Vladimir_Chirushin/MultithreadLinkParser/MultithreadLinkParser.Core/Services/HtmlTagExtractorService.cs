namespace MultithreadLinkParser.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;

    public class HtmlTagExtractorService : IHtmlTagExtractorService
    {
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