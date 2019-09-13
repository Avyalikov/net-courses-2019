namespace WikipediaParser
{
    using StructureMap;
    using WikipediaParser.Services;

    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container(new WikipediaParserRegistry());
            WikipediaParsingService wiki = container.GetInstance<WikipediaParsingService>();
            wiki.Start("https://en.wikipedia.org");
        }
    }
}
