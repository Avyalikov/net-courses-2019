using StructureMap;
using System;
using WikipediaParser.Services;

namespace WikipediaParser
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            using (var wiki = new WikiParsingDbContext())
            {
                wiki.Database.EnsureCreated();

                wiki.Links.Add(new Models.LinkEntity { Link ="vk.com", IterationId = 1 });
                wiki.SaveChanges();
            }
            */
            var container = new Container(new WikipediaParserRegistry());
            WikipediaParsingService wiki = container.GetInstance<WikipediaParsingService>();
            wiki.Start("https://en.wikipedia.org/wiki/Prequel");
        }
    }
}
