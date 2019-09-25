using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IUrlService
    {
        void AddUrl(UrlEntity urlEntity);
        void AddUrlDictionary(System.Collections.Generic.Dictionary<string, int> urls);
        UrlEntity GetUrl(string Url);
        void RemoveUrl(string Url);
    }
}