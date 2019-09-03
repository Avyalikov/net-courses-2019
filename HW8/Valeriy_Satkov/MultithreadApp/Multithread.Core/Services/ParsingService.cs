namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;

    public class ParsingService
    {
        private ILinkTableRepository linkTableRepository;

        public ParsingService(ILinkTableRepository linkTableRepository)
        {
            this.linkTableRepository = linkTableRepository;
        }

        public async Task<string> DownloadPage(string link, HttpMessageHandler handler)
        {
            using (var client = new HttpClient(handler))
            {
                using (var response = await client.GetAsync(link))
                {
                    using (var content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();
                        return jsonString;
                    }
                }
            }
        }

        public List<string> ExtractLinksFromHtmlString(string startPageHost, string htmlContent)
        {
            /* regular
             * ver.1
             * <a href="(https:\/\/awaps\.yandex\.net\/.*)"
             * ver.2
             * string startPageHost = "https://en.wikipedia.org"
             * $"<a href=\"({startPageHost}.*)"
             */
            List<string> resultList = new List<string>();

            Regex regex = new Regex($"<a.href=\"({startPageHost}.*)");
            MatchCollection matches = regex.Matches(htmlContent);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    resultList.Add(match.Value);
                }                    
            }

            return resultList;
        }

        public int Save(string link, int iterationId)
        {
            SaveValidation(link, iterationId);

            ContainsByLink(link);

            var entityToAdd = new LinkEntity()
            {
                Link = link,
                IterationId = iterationId
            };

            this.linkTableRepository.Add(entityToAdd);

            this.linkTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void SaveValidation(string link, int iterationId)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                throw new ArgumentException("'link' is null, empty or consists only of white-space characters");
            }

            if (iterationId <= 0)
            {
                throw new ArgumentException("'iterationId' is zero or negative");
            }
        }

        public void ContainsByLink(string link)
        {
            if (this.linkTableRepository.ContainsByLink(link))
            {
                throw new ArgumentException("This link has been registered. Can't continue.");
            }
        }
    }
}
