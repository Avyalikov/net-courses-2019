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
            UrlCollectorService urlCollectorService = new UrlCollectorService(this.saver, this.downloader, this.cleaner);
            Console.WriteLine("Select the way of program's work:");
            Console.WriteLine("1 - One thread.");
            Console.WriteLine("2 - Multithread.");
            int userInput;
            int.TryParse(Console.ReadLine(), out userInput);
            Task<string> result = null;
            switch (userInput)
            {
                case 1:
                    result = urlCollectorService.Solothread(startPageToParse, baseUrl);
                    break;
                case 2:
                    result = urlCollectorService.Multithread(startPageToParse, baseUrl);
                    break;
                default:
                    Console.WriteLine("Unknown command. Input any key to exit.");
                    break;
            }
            Console.WriteLine("Awaiting for async methods...");
            Console.WriteLine(result.Result);
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
                this.cleaner.DeleteDirectory();
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
