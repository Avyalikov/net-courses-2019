using System.Collections.Generic;
using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IUrlParsingService
    {
        IEnumerable<UrlEntity> ExtractAllUrlsFromPage(string rawDocument, int iteration);
    }
}