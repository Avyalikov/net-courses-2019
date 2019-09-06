using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Models;
using WikiURLCollector.Core.Repositories;
using WikiURLCollector.Core.Interfaces;

namespace WikiURLCollector.Core.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository urlRepository;
        public UrlService(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository;
        }
        public void AddUrl(UrlEntity urlEntity)
        {
            var url = urlRepository.GetUrlEntity(urlEntity.URL);
            if (url != null)
            {
                return;
            }
            urlRepository.AddUrlEntity(urlEntity);
            urlRepository.SaveChanges();
        }
        public void RemoveUrl(string Url)
        {
            urlRepository.RemoveUrlEntity(Url);
            urlRepository.SaveChanges();
        }
        public UrlEntity GetUrl(string Url)
        {
            return urlRepository.GetUrlEntity(Url);
        }

        public void AddUrlDictionary(Dictionary<string, int> urls)
        {
            var urlsFromDB = urlRepository.GetAllUrls();
            foreach (var url in urls)
            {
                if (!urlsFromDB.Any(x => x.URL == url.Key))
                {
                    var urlEntity = new UrlEntity() { URL = url.Key, IterationId = url.Value };
                    AddUrl(urlEntity);
                }
            }
        }
    }
}
