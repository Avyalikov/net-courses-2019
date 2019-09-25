namespace Multithread.Console
{
    using System;
    using Multithread.Console.DependencyInjection;
    using Multithread.Core.Services;
    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {           
            int endIteration = 3;
            string url = "https://en.wikipedia.org/wiki/Mummia";
            var linkProcessingService = new Container(new AppRegistry()).GetInstance<LinkProcessingService>();

            linkProcessingService.SingleThread(url);

            linkProcessingService.ParsingForEachPage(endIteration);

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
