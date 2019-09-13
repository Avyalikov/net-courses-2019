namespace MultithreadLinkParser.Console.DependencyInjection
{
    using MultithreadLinkParser.Console.Repositories;
    using MultithreadLinkParser.Core.Repositories;
    using MultithreadLinkParser.Core.Services;
    using StructureMap;

    public class MultithreadLinkParserRegistry : Registry
    {
        public MultithreadLinkParserRegistry()
        {
            this.For<IParsingEngine>().Use<ParsingEngine>();

            this.For<IExtractionManager>().Use<ExtractionManager>();
            this.For<IHtmlTagExtractorService>().Use<HtmlTagExtractorService>();
            this.For<IPageDownloaderService>().Use<PageDownloaderService>();
            this.For<ITagsDataBaseManager>().Use<TagsDataBaseManager>();
            this.For<ITagsRepository>().Use<TagsRepository>();
        }
    }
}
