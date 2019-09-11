using StructureMap;
using System;
using WikipediaParser.Services;

namespace WikipediaParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new WikipediaParserRegistry());
            WikipediaParsingService wiki = container.GetInstance<WikipediaParsingService>();
            wiki.Start("https://en.wikipedia.org");
        }
    }
}
