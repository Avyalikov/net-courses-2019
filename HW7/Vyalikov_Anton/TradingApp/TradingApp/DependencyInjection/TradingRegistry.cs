namespace TradingApp.DependencyInjection
{
    using TradingApp.Interfaces;
    using TradingApp.Services;
    using StructureMap;

    class TradingRegistry : Registry
    {
        public TradingRegistry()
        {
            this.For<IInputOutputModule>().Use<InputOutputModule>();
            this.For<ICommandParser>().Use<CommandParser>();
            this.For<IMainLoop>().Use<MainLoop>();
            this.For<IHTTPRequestService>().Use<HTTPRequestService>();
        }
    }
}
