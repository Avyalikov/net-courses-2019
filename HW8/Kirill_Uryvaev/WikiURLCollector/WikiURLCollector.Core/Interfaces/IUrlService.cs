using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IUrlService
    {
        void AddUrl(UrlEntity urlEntity);
        UrlEntity GetUrl(string Url);
        void RemoveUrl(string Url);
    }
}