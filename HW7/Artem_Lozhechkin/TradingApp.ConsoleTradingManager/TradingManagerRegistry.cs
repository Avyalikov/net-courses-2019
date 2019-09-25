namespace TradingApp.ConsoleTradingManager
{
    using StructureMap;
    using System.Configuration;
    using TradingApp.Shared;

    public class TradingManagerRegistry : Registry
    {
        public TradingManagerRegistry()
        {
            this.For<RequestSender>().Use<RequestSender>();
            this.For<TradingAppDbContext>().Use<TradingAppDbContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingAppConnectionString"].ConnectionString);
            this.For<ConsoleManager>().Use<ConsoleManager>();
        }
    }
}
