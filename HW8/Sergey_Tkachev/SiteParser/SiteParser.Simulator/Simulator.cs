namespace SiteParser.Simulator
{
    using System;
    using System.Threading.Tasks;
    using SiteParser.Core.Repositories;
    using SiteParser.Core.Services;

    public class Simulator : ISimulator, IDisposable
    {
        private readonly ISaver saver;
        private readonly IDownloader downloader;
        private readonly ICleaner cleaner;

        private bool disposed = false; // to detect redundant calls

        /// <summary>
        /// Initializes a new instace of the Simulator class.
        /// </summary>
        /// <param name="saver">Object for saving into DataBase.</param>
        /// <param name="downloader">Object for downloading pages info file.</param>
        /// <param name="cleaner">Object for deletion files and folder.</param>
        public Simulator(ISaver saver, IDownloader downloader, ICleaner cleaner)
        {
            this.saver = saver;
            this.downloader = downloader;
            this.cleaner = cleaner;
        }

        /// <summary>
        /// Start simulation process
        /// </summary>
        /// <param name="startPageToParse">Initial Url</param>
        /// <param name="baseUrl">Base Url</param>
        public void Start(string startPageToParse, string baseUrl)
        {
            UrlCollectorService urlCollectorService = new UrlCollectorService(this.saver, this.downloader, this.cleaner);
            Console.WriteLine("Select the way of program's work:");
            Console.WriteLine("1 - One thread.");
            Console.WriteLine("2 - Multithread.");
            int.TryParse(Console.ReadLine(), out int userInput);
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
                    return;
            }

            Console.WriteLine("Awaiting for async methods...");
            Console.WriteLine(result.Result);
        }

        /// <summary>
        /// Finalizes an instance of the Simulator class.
        /// </summary>
        ~Simulator()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Method to manual call.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Base method.
        /// </summary>
        /// <param name="disposing">True - manual call. False - finalizer call.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose-only, i.e. non-finalizable logic
                }

                // shared cleanup logic
                this.cleaner.DeleteDirectory();
                this.disposed = true;
            }
        }
    }
}
