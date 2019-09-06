namespace Traiding.ConsoleApp
{
    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {
            new StockExchange(
                new Container(new DependencyInjection.TraidingRegistry())
                ).Start();
        }
    }
}
