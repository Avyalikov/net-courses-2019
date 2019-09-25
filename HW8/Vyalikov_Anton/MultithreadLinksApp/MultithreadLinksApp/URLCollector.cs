namespace MultithreadLinksApp
{
    using MultithreadLinksApp.Core.Interfaces;
    using MultithreadLinksApp.Core.Services;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    class URLCollector
    {
        private readonly IURLCollectingService collectingService;
        private readonly URLService urlService;

        public URLCollector(IURLCollectingService collectingService, URLService urlService)
        {
            this.collectingService = collectingService;
            this.urlService = urlService;
        }

        public async Task CollectUrls(string url, int recursionDepth)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine($"{DateTime.Now} Start parsing URL.");
            watch.Start();
            var urlsDict = await collectingService.CollectURLs(url, recursionDepth);
            watch.Stop();
            Console.WriteLine($"{DateTime.Now} Parsing with depth - {recursionDepth} is completed in {watch.Elapsed}.");
            Console.WriteLine($"{DateTime.Now} Saving URL to database.");
            watch.Restart();
            urlService.AddURLFromDict(urlsDict);
            watch.Stop();
            Console.WriteLine($"{DateTime.Now} Saving to database was completed in {watch.Elapsed}");
        }
    }
}
