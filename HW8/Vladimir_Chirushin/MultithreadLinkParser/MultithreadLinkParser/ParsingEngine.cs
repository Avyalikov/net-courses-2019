namespace MultithreadLinkParser
{
    using MultithreadLinkParser.Models;
    using MultithreadLinkParser.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class ParsingEngine : IParsingEngine
    {
        private readonly ILinkParserService linkParserService;

        public ParsingEngine(ILinkParserService linkParserService)
        {
            this.linkParserService = linkParserService;
        }

        List<LinkInfo> linkInfo = new List<LinkInfo>();

        public void Run(string startingUrl)
        {
            CancellationToken cts = new CancellationToken();

            Console.WriteLine("Starting!");

            linkParserService.AccessTheWebAsync(linkInfo, new List<string> { startingUrl }, cts, 0);
           
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {

            }
        }
    }
}