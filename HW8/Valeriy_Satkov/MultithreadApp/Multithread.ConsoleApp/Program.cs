namespace Multithread.ConsoleApp
{
    using Multithread.ConsoleApp.DependencyInjection;
    using Multithread.Core.Services;
    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new Container(new ConnectedLinksRegistry()))
            {
                new ConnectedLinksParser(container.GetInstance<ParsingService>(), container.GetInstance<ReportsService>())
                    .Start();
            }            
        }
    }
}
