namespace TradingSimulator
{
    using StructureMap;
    using Core;
    using Components;
    using Interfaces;

    class TradingSimulatorRegistry : Registry
    {
        public TradingSimulatorRegistry()
        {
            this.For<IInputOutput>().Use<ConsoleIO>();
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            
            this.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().Get());

            this.For<IGame>().Use<Game>();
        }
    }
}