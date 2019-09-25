using LinkDBContext;
using LinkDBContext.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlLinksCore.Repository;
using UrlLinksCore.Services;
using System.Threading;
using UrlLinksCore.IService;

namespace Links
{
    class Program
    {
        static void Main(string[] args)
        {
            IDownloadService downloadService = new DownloadService();
            IParserService parserService = new ParserService();
            DownloadWorker downloader = new DownloadWorker(downloadService, parserService);
            Task start = downloader.RunRecursively(3, "https://en.m.wikipedia.org/wiki/Serbs");
            start.Wait();

        }
    }
}
