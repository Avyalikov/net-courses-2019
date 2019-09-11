namespace MultithreadLinkParser.Console.DependencyInjection
{
    using MultithreadLinkParser.Core.Services;
    using MultithreadLinkParser.Core.Repositories;
    using StructureMap;
    using MultithreadLinkParser.Console.Repositories;

    class MultithreadLinkParserRegistry : Registry
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
