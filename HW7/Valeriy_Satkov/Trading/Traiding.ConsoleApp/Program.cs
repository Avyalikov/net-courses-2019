namespace Traiding.ConsoleApp
{
    using StructureMap;
    using Traiding.ConsoleApp.Logger;

    class Program
    {
        static void Main(string[] args)
        {
            ILoggerService loggerService = new LoggerService();

            loggerService.InitLogger(); // Initialization
            loggerService.Log.Info("Start logging");

            new StockExchange(
                new Container(new DependencyInjection.TraidingRegistry())
                ).Start();
        }
    }
}
