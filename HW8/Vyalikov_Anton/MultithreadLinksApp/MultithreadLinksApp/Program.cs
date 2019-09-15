namespace MultithreadLinksApp
{
    using StructureMap;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new LinksAppRegistry());
            var urlCollector = container.GetInstance<URLCollector>();
            string exitCode = "exit";
            string userInput = "";
            int recursionDepth = 2;
            Console.WriteLine($"{DateTime.Now} Program started");
            while (!userInput.ToLower().Equals(exitCode))
            {
                userInput = Console.ReadLine();
                urlCollector.CollectUrls(userInput, recursionDepth);
            }
        }
    }
}
