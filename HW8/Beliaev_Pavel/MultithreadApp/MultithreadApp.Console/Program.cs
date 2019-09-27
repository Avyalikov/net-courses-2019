using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadService downloadService = new DownloadService();
            ParsingService parserService = new ParsingService();
            App app = new App(downloadService, parserService);

            int depth = 2;

            Task task = app.Run("https://en.m.wikipedia.org/wiki/Batman", depth);
            task.Wait();
        }
    }
}
