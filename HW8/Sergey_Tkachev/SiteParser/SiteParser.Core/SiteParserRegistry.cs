using SiteParser.Core.Services;
using StructureMap;

namespace SiteParser.Core
{
    public class SiteParserRegistry : Registry
    {
        public SiteParserRegistry()
        {
            this.For<CallParsingFromPreviousIterationService>().Use<CallParsingFromPreviousIterationService>();
            this.For<DownloadPageService>().Use<DownloadPageService>();
            this.For<ParsePageService>().Use<ParsePageService>();
            this.For<SaveIntoDatabaseService>().Use<SaveIntoDatabaseService>();
        }
    }
}
