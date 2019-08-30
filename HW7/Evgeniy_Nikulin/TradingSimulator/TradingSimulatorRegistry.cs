namespace TradingSimulator
{
    using StructureMap;
    using Core;
    using Core.Interfaces;
    using Components;

    class TradingSimulatorRegistry : Registry
    {
        public  TradingSimulatorRegistry()
        {
            this.For<IController>().Use<Controller>();
            For<IInputOutput>().Use<ConsoleIO>();
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<ILoggerService>().Use<LoggerService>();

            this.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().Get());
        }
    }
}