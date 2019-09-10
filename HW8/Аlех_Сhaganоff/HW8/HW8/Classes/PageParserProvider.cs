using HW8.Intefaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class PageParserProvider : IPageParserProvider
    {
        public List<string> GetLinks(string data)
        {
            List<string> listOfLinks = new List<string>();

            MatchCollection hrefs = Regex.Matches(data, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

            foreach (Match match in hrefs)
            {
                string value = match.Groups[1].Value;
                Match links = Regex.Match(value, @"href=\""/(.*?)\""", RegexOptions.Singleline);

                if (links.Success)
                {
                    listOfLinks.Add(links.Groups[1].Value);
                }
            }

            return listOfLinks;
        }
    }
}
