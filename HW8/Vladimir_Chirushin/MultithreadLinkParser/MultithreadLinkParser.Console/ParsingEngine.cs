namespace MultithreadLinkParser.Console
{
    using System;
    using System.Threading;
    using MultithreadLinkParser.Core.Services;

    public class ParsingEngine : IParsingEngine
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

            this.extractionManager.RecursionTagExtraction(startingUrl, 0, cts);
            Console.ReadLine();
        }
    }
}