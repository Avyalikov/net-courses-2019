using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace WikipediaParser.Services
{
    public class PageParsingService
    {
        public List<string> ParseForLinks(string htmlSource)
        {
            List<string> links = new List<string>();

            XElement content = XElement.Parse(htmlSource);
            foreach (XElement obs in content.Descendants("a"))
            {
                var link = obs.Attribute("href");
                if (link != null && (link.Value.StartsWith("/w") || link.Value.Contains("/en.wikipedia.org")))
                {
                    if (link.Value.StartsWith("/w"))
                        links.Add("https://en.wikipedia.org" + link.Value);
                    else if (link.Value.StartsWith("//"))
                        links.Add("https:" + link.Value);
                    else links.Add(link.Value);
                }
            }
            return links;
        }
    }
}
