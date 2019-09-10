using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Repositories
{
    public interface IUrlRepository: IBaseRepository
    {
        UrlEntity GetUrlEntity(string Url);
        void AddUrlEntity(UrlEntity urlEntity);
        void UpdateUrlEntity(UrlEntity urlEntity);
        void RemoveUrlEntity(string Url);
        IEnumerable<UrlEntity> GetAllUrls();
    }
}
