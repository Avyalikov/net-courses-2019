namespace ReferenceCollectorApp
{
    using ReferenceCollector.Core.Services;
    using ReferenceCollectorApp.Services;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading;
    using System.Threading.Tasks;
    class WikiReferencesCollector : IWikiReferenceCollector
    {
        private readonly string folderPath;
        private readonly int maxIterations;
        private readonly string startPage;
        private readonly IPageDownloadService pageDownloadService;
        private readonly IFileSystemService fileSystemService;
        private readonly IWikiReferencesParsingService wikiReferencesParsingService;
        private readonly IReferencesDbService referencesDbService;
        private readonly IIoDevice ioDevice;
        private readonly Mutex mutexObj;
        private readonly bool canRun;
        private readonly object dbLock;

        public WikiReferencesCollector(
            IPageDownloadService pageDownloadService, 
            IFileSystemService fileSystemService, 
            IWikiReferencesParsingService wikiReferencesParser,
            IReferencesDbService referencesDbService,
            IIoDevice ioDevice)
        {
            this.folderPath = ConfigurationManager.AppSettings["FolderPath"];
            this.maxIterations = int.Parse(ConfigurationManager.AppSettings["MaxIterations"]);
            this.startPage = ConfigurationManager.AppSettings["StartPage"];
            this.pageDownloadService = pageDownloadService;
            this.pageDownloadService.BaseAdress = ConfigurationManager.AppSettings["Uri"];
            this.fileSystemService = fileSystemService;
            this.wikiReferencesParsingService = wikiReferencesParser;
            this.referencesDbService = referencesDbService;
            this.ioDevice = ioDevice;
            this.mutexObj = new Mutex(true, "ReferenceCollector", out canRun);
            dbLock = new object();
        }

        public void Run()
        {
            if (!canRun)
            {
                ioDevice.Print("The application is already running. Press any key to quit");
                ioDevice.ReadKey();
                return;
            }
            
            var startDictionary = new Dictionary<string, int>() { { startPage, 0 } };
            WriteReferencesRecursively(startDictionary, 0);

            ioDevice.Print("Completed");
            ioDevice.ReadKey();
        }

        private void WriteReferencesRecursively(Dictionary<string, int> pages, int iterationId)
        {
            iterationId++;
            if (iterationId >= maxIterations || pages.Count<1)
            {
                return;
            }

            Parallel.ForEach(pages, (e) =>
            {
                Thread.Sleep(500);
                var page = pageDownloadService.DownloadPage(e.Key);
                if (string.IsNullOrEmpty(page.Result))
                {
                    return;
                }

                var filePath = fileSystemService.WriteDataToFile(page.Result, folderPath);
                var references = wikiReferencesParsingService.ParseRefsFromFileToDictionary(filePath, iterationId);
                fileSystemService.DeleteFile(filePath);

                lock (dbLock)
                {
                    referencesDbService.WriteRefsToDb(ref references);
                }

                WriteReferencesRecursively(references, iterationId);
            }
            );
        }
    }
}
