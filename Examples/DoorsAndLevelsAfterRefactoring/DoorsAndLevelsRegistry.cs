using StructureMap;

namespace DoorsAndLevelsAfterRefactoring
{
    public class DoorsAndLevelsRegistry : Registry
    {
        public DoorsAndLevelsRegistry()
        {
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<IInputOutputDevice>().Use<ConsoleInputOutputDevice>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<IDoorsNumbersGenerator>().Use<DoorsNumbersGenerator>();
            this.For<IGame>().Use<Game>();

            this.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().GetGameSettings());
        }
    }
}
