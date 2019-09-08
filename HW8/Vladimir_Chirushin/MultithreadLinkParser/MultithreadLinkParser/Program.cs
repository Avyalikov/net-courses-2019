namespace MultithreadLinkParser
{
    using System;
    using MultithreadLinkParser.DependencyInjection;
    using StructureMap;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container(new MultithreadLinkParserRegistry());
            var parsingEngine = container.GetInstance<IParsingEngine>();

            Console.WriteLine("Enter target link:");
            var startingUrl = Console.ReadLine();

            parsingEngine.Run(startingUrl);
        }
    }
}