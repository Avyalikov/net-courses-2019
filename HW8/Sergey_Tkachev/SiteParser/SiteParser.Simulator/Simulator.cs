using SiteParser.Core.Repositories;
using SiteParser.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    public class Simulator : ISimulator
    {
        private readonly ISaver saver;
        private readonly IDownloader downloader;

        public Simulator(ISaver saver, IDownloader downloader)
        {
            this.saver = saver;
            this.downloader = downloader;
        }

        public void Start(string startPageToParse, string baseUrl)
        {
            UrlCollectorService urlCollectorService = new UrlCollectorService(saver, downloader);
            urlCollectorService.InitialDowload(startPageToParse, baseUrl);
        }
    }
}
