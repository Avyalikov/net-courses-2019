using SiteParser.Core.Repositories;
using SiteParser.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Simulator
{
    public class Simulator : ISimulator, IDisposable
    {
        private readonly ISaver saver;
        private readonly IDownloader downloader;
        private readonly ICleaner cleaner;

        private bool disposed = false; // to detect redundant calls

        public Simulator(ISaver saver, IDownloader downloader, ICleaner cleaner)
        {
            this.saver = saver;
            this.downloader = downloader;
            this.cleaner = cleaner;
        }

        public void Start(string startPageToParse, string baseUrl)
        {
            UrlCollectorService urlCollectorService = new UrlCollectorService(this.saver, this.downloader);
            urlCollectorService.InitialDowload(startPageToParse, baseUrl);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // dispose-only, i.e. non-finalizable logic
                }

                // shared cleanup logic
                this.cleaner.Clean();
                disposed = true;
            }
        }

        ~Simulator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
