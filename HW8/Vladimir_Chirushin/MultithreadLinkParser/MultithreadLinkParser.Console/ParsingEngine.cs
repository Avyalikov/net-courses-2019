namespace MultithreadLinkParser.Console
{
    using MultithreadLinkParser.Core.Models;
    using MultithreadLinkParser.Core.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class ParsingEngine : IParsingEngine
    {
        private readonly IExtractionManager extractionManager;

        public ParsingEngine(IExtractionManager extractionManager)
        {
            this.extractionManager = extractionManager;
        }


        public void Run(string startingUrl)
        {
            CancellationToken cts = new CancellationToken();

            Console.WriteLine("Starting!");

            extractionManager.MyRecAsync(startingUrl, 0, cts);
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {

            }
        }
    }
}