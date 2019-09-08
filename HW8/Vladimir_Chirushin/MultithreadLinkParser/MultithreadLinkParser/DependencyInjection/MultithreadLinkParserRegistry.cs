namespace MultithreadLinkParser.DependencyInjection
{
    using MultithreadLinkParser.Repositories;
    using MultithreadLinkParser.Services;
    using StructureMap;

    class MultithreadLinkParserRegistry : Registry
    {
        public MultithreadLinkParserRegistry()
        {
            this.For<IParsingEngine>().Use<ParsingEngine>();

            this.For<ILinksRepository>().Use<LinksRepository>();
            this.For<ILinkToDBManager>().Use<LinkToDBManager>();
            this.For<ILinkParserService>().Use<LinkParserService>();
        }
    }
}
