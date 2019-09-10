using System;
using System.Collections.Generic;


namespace MultithreadConsoleApp.Interfaces
{
    public interface IHtmlParser
    {
        List<string> FindLinksFromHtml(string html);
    }
}
