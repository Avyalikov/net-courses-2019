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

        public void ParsingThePageCsQuery(string href)
        {
                WebClient webClient = new WebClient();

                string html = webClient.DownloadString(href);

                CQ cq = CQ.Create(html);

                foreach (IDomObject obj in cq.Find("a[href^='/wiki/']"))
                {
                    LinksHistoryEntity linksHistoryEntity = new LinksHistoryEntity() { Links = "https://en.wikipedia.org" + obj.GetAttribute("href"), PreviousLink = href };

                    if (!linksHistoryServices.ContainsLinks(linksHistoryEntity))
                    {
                        linksHistoryServices.RegisterNewLinks(linksHistoryEntity);
                    }
                }   

            //    List<string> hrefTags = new List<string>();

            //    WebClient webClient = new WebClient();

            //    string html = webClient.DownloadString("https://en.wikipedia.org/wiki/Main_Page");

            //    CQ cq = CQ.Create(html);

            //    foreach (IDomObject obj in cq.Find("a[href^='/wiki/']"))

            //    {
            //        hrefTags.Add(obj.GetAttribute("href"));
            //    }

            //    return hrefTags;
        }


        public List<string> CsQuery()
        {
            List<string> hrefTags = new List<string>();

            WebClient webClient = new WebClient();

            string html = webClient.DownloadString("https://en.wikipedia.org/wiki/Takeoff");

            CQ cq = CQ.Create(html);

            foreach (IDomObject obj in cq.Find("a[href^='/wiki/']"))

            {
                hrefTags.Add(obj.GetAttribute("href"));
            }

            return hrefTags;
        }

        public void Cccc(object s)
        {
            Console.WriteLine("что то печаиаю !!!" + (string)s);
        }
    }
}
