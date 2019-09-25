namespace WikipediaParser
{
    using StructureMap;
    using System.Configuration;
    using WikipediaParser.Services;

    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container(new WikipediaParserRegistry());
            WikipediaParsingService wiki = container.GetInstance<WikipediaParsingService>();
            string startUrl = ConfigurationManager.AppSettings.Get("startUrl");
            wiki.Start(startUrl);
        }
    }
}
