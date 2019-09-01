using System;
using System.Collections.Generic;
using System.Text;
using WikipediaParser.Repositories;
using WikipediaParser.Models;

namespace WikipediaParser.Services
{
    public class WikipediaParsingService
    {
        private readonly LinksTableRepository linksTableRepository;
        private readonly DownloadingService downloadingService;
        private readonly PageParsingService pageParsingService;
        private string baseAddress;

        public WikipediaParsingService(LinksTableRepository linksTableRepository, DownloadingService downloadingService, PageParsingService pageParsingService)
        {
            this.linksTableRepository = linksTableRepository;
            this.downloadingService = downloadingService;
            this.pageParsingService = pageParsingService;
        }
        public void Start(string baseUrl)
        {
            this.baseAddress = baseUrl;
            var htmlSource = this.DownloadHTMLSourceViaHtml(this.baseAddress);
            var links = this.ParseHTML(htmlSource);

            foreach(var item in links)
            {
                var link = new LinkEntity { Link = item, IterationId = 1 };
                if (!this.linksTableRepository.ContainsByUrl(link))
                    this.linksTableRepository.Add(new LinkEntity { Link = item, IterationId = 1});
            }
            
        }

        private List<string> ParseHTML(string htmlSource)
        {
            return this.pageParsingService.ParseForLinks(htmlSource);
        }

        public string DownloadHTMLSourceViaHtml(string url)
        {
            var s = this.downloadingService.GetHTMLSource(url);
            //Console.WriteLine(s);
            return s;
        }

    }
}
