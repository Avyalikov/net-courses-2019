using CsQuery;
using Multithread.ConsoleApp.Data;
using Multithread.ConsoleApp.Repositories;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Multithread.ConsoleApp.Components
{
    
    public class ParserPages
    {
        private readonly MultithreadDbContext multithreadDbContext;
        private readonly ILinksHistoryRepositoroes linksHistoryRepositoroes;
        private readonly LinksHistoryServices linksHistoryServices;

        public ParserPages(
            MultithreadDbContext multithreadDbContext, 
            ILinksHistoryRepositoroes linksHistoryRepositoroes, 
            LinksHistoryServices linksHistoryServices)
        {
            this.multithreadDbContext = multithreadDbContext;
            this.linksHistoryRepositoroes = linksHistoryRepositoroes;
            this.linksHistoryServices = linksHistoryServices;
        }

        public void ParsingThePageCsQuery(object href)
        {
                WebClient webClient = new WebClient();

                string htmlPage = webClient.DownloadString((string)href);

                CQ parserObject = CQ.Create(htmlPage);

                foreach (IDomObject obj in parserObject.Find("a[href^='/wiki/']"))
                {
                    LinksHistoryEntity linksHistoryEntity = new LinksHistoryEntity() { Links = "https://en.wikipedia.org" + obj.GetAttribute("href"), PreviousLink = (string)href };

                    if (!linksHistoryServices.ContainsLinks(linksHistoryEntity))
                    {
                        linksHistoryServices.RegisterNewLinks(linksHistoryEntity);
                    }
                }   
        }


        public List<string> GettingTheInitialListOfLinks()
        {
            List<string> hrefTags = new List<string>();

            WebClient webClient = new WebClient();

            string htmlPage = webClient.DownloadString("https://en.wikipedia.org/wiki/Takeoff");

            CQ parserObject = CQ.Create(htmlPage);

            foreach (IDomObject obj in parserObject.Find("a[href^='/wiki/']"))

            {
                hrefTags.Add(obj.GetAttribute("href"));
            }

            return hrefTags;
        }

    }
}
