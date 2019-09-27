using MultithreadApp.Core.DTO;
using MultithreadApp.Core.Model;
using MultithreadApp.Core.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Services
{
    public class UrlService
    {
        private readonly IUnitOfWork unitOfWork;
        public UrlService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }

        public void AddLinkToDB(UrlInfo urlInfo)
        {
            if (this.ContainsLink(urlInfo.Link))
            {
                return;
            }

            Url linkToAdd = new Url()
            {
                Link = urlInfo.Link,
                IterationId = urlInfo.IterationId

            };
            this.unitOfWork.Urls.Add(linkToAdd);
            this.unitOfWork.Save();
        }

        public void AddParsedLinksToDB(List<string> urls, int iteration)
        {
            foreach (string url in urls)
            {
                UrlInfo urlInfo = new UrlInfo()
                {
                    Link = url,
                    IterationId = iteration
                };
                this.AddLinkToDB(urlInfo);
            }
        }



        public bool ContainsLink(string url)
        {
            return this.unitOfWork.Urls.GetByCondition(l => l.Link.Equals(url, StringComparison.OrdinalIgnoreCase)).Any();
        }

        public IEnumerable<Url> GetAllLinks()
        {
            return this.unitOfWork.Urls.GetAll().ToList();
        }


        public IEnumerable<String> GetAllLinksByIteration(int iterationId)
        {
            return this.unitOfWork.Urls.GetByCondition(l => l.IterationId == iterationId).Select(l => l.Link).ToList();
        }

        public int GetCurrentIteration()
        {            
            int dbiteration;
            List<int> iterations = GetIterations().ToList();
            if (iterations.Count() == 0)
            {
                dbiteration = 1;
            }
            else
            {
                dbiteration = iterations.Last() + 1;
            }
            return dbiteration;
        }

        public IEnumerable<int> GetIterations()
        {
            var links = this.GetAllLinks().ToList();
            IEnumerable<int> iterations = links.OrderBy(o=>o.IterationId).Select(i => i.IterationId).Distinct().ToList();
            return iterations;
        }
    }
}
