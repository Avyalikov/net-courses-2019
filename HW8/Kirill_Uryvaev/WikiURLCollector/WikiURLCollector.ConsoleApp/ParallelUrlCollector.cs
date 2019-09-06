using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;
using WikiURLCollector.Core.Models;
using WikiURLCollector.Core.Services;

namespace WikiURLCollector.ConsoleApp
{
    public class ParallelUrlCollector
    {
        private readonly IParallelUrlCollectingService parallelUrlCollectingService;

        public ParallelUrlCollector(IParallelUrlCollectingService parallelUrlCollectingService)
        {
            this.parallelUrlCollectingService = parallelUrlCollectingService;
        }

        public async Task GetUrls(string userInput, int maxIterations)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine($"{DateTime.Now} Start url parsing");
            watch.Start();
            var urlsDictionary = await parallelUrlCollectingService.GetUrls(userInput, maxIterations);
            watch.Stop();
            Console.WriteLine($"Parsing with derph {maxIterations} is completed in {watch.Elapsed}");
        }

    }
}
