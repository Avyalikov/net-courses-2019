using ReferenceCollectorApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceCollectorApp
{
    class ReferenceCollector : IReferenceCollector
    {
        private readonly IReferenceCollectorService service;
        private Dictionary<string, int> fileredRefs;
        private readonly string folderPath;
        private readonly int maxIterations;
        private readonly string startPage;
        //private List<Task<Dictionary<string, int>>> tasks;
        public ReferenceCollector(IReferenceCollectorService service)
        {
            this.service = service;
            this.fileredRefs = new Dictionary<string, int>();
            this.folderPath = ConfigurationManager.AppSettings["FolderPath"];
            this.maxIterations = int.Parse(ConfigurationManager.AppSettings["MaxIterations"]);
            this.startPage = ConfigurationManager.AppSettings["StartPage"];
            //tasks = new List<Task<Dictionary<string, int>>>();
        }

        public void Run()
        {
            fileredRefs.Add(startPage, 0);
            WriteRefs(fileredRefs, 0);

            Console.WriteLine("Completed");
            Console.ReadKey();
        }

        private async Task WriteRefs(Dictionary<string, int> pages, int iterationId)
        {
            iterationId++;
            if (iterationId >= maxIterations || pages.Count<1)
            {
                return;
            }

            Parallel.ForEach(pages, (e) =>
            {
                var a = service.DownloadPage(e.Key, folderPath);
                var b = service.AddRefsToDictionary(a.Result, iterationId/*, fileredRefs*/);
                service.WriteDictionaryToDb(b);
                WriteRefs(b, iterationId);
            }
            );
        }
    }
}
