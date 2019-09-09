using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MultithreadConsoleApp.Components
{
    public static class HtmlParser
    {
        public static List<string> FindLinksFromStr(string inputStream)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(inputStream);
            var linkCollection = doc.DocumentNode.SelectNodes("//a[@href]");
            List<string> result = new List<string>();
            foreach (var node in linkCollection)
            {
                var link = node.Attributes["href"];
                if (link.Value.Contains("wikipedia.org") && link.Value.Contains("https://"))
                {
                    result.Add(link.Value);
                }

            }
            return result;
        }
    }
}
   