using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using MultithreadConsoleApp.Interfaces;
namespace MultithreadConsoleApp.Components
{
    public class HtmlParser : IHtmlParser
    { 
        public List<string> FindLinksFromHtml(string html)
        {
            List<string> result = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var linkCollection = doc.DocumentNode.SelectNodes("//a[@href]");
            if (linkCollection == null)
                return result;
            foreach (var node in linkCollection)
            {
                var link = node.Attributes["href"];
                if (link.Value.StartsWith("/wiki/"))
                {
                    result.Add("https://en.wikipedia.org" + link.Value);
                }

            }
            return result;
        }
    }
}
   