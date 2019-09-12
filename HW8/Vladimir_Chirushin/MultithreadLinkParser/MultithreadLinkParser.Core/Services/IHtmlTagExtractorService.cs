namespace MultithreadLinkParser.Core.Services
{
    using System.Collections.Generic;

    public interface IHtmlTagExtractorService
    {
        List<string> ExtractTags(string rawHttpData, string urlToParse);
    }
}