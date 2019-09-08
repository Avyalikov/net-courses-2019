namespace Multithread.Console.Repo
{
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Multithread.Core.Repo;
    using System;

    public class WebRepo : IWebRepo
    {
        public HtmlDocument DowloandPage(string url)
        {
            Thread.Sleep(10);
            return new HtmlWeb().Load(url);
        }

        public IEnumerable<string> GetAllTagsFromPage(HtmlDocument page)
        {
            return page.DocumentNode
                                 .Descendants("a")
                                 .Select(a => a.GetAttributeValue("href", null))
                                 .Where(u => !String.IsNullOrEmpty(u));
        }
    }
}
