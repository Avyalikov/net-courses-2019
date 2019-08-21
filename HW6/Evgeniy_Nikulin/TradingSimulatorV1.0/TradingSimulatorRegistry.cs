namespace TradingSimulator
{
    using Components;
    using Core;
    using Core.Components;
    using Core.Interfaces;
    using log4net;
    using StructureMap;

    class TradingSimulatorRegistry : Registry
    {
        public  TradingSimulatorRegistry()
        {
            this.For<IController>().Use<Controller>();
            this.For<IInputOutput>().Use<ConsoleIO>();
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<IDbController>().Use<DbController>();
            this.For<ITradeMenager>().Use<TradeManager>();
            this.For<ITradersManager>().Use<TradersManager>();
            this.For<ILoggerService>().Use<LoggerService>();

            this.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().Get());
        }
    }
}