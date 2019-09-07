namespace Multithread.Console
{
    using System;
    using Multithread.Console.Repo;
    using Multithread.Core.Services;

    class Program
    {
        static void Main(string[] args)
        {
            int endIteration = 3;
            string url = "https://en.wikipedia.org/wiki/Mummia";
            var linkService = new LinksServices(new LinksRepo());

            linkService.SingleThread(url);
            
            linkService.ParsingForEachPage(endIteration);

            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
