using System.Collections.Generic;
using System.Threading;

namespace MultithreadLinkParser.Core.Services
{
    public interface IHtmlTagExtractorService
    {
        List<string> ExtractTags(string rawHttpData, string urlToParse);
    }
}