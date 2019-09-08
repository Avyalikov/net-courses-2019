namespace Multithread.Core.Repo
{
    using HtmlAgilityPack;
    using System.Collections.Generic;

    public interface IWebRepo
    {
        HtmlDocument DowloandPage(string url);
        IEnumerable<string> GetAllTagsFromPage(HtmlDocument page);
    }
}
