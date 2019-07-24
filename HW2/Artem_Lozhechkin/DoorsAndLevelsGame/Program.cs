namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initializing game components
            SimpleRandomIntArrayGenerator simpleRandomLongArrayGenerator = new SimpleRandomIntArrayGenerator();
            SimpleStackDataStorage<int> simpleStackDataStorage = new SimpleStackDataStorage<int>();
            GameConsole gameConsole = new GameConsole();
            SimpleSettingsProvider simpleSettingsProvider = new SimpleSettingsProvider();
            SimplePhraseProvider simplePhraseProvider = new SimplePhraseProvider(simpleSettingsProvider.GetSettings().language);

            Game game = new Game(simpleRandomLongArrayGenerator, simpleStackDataStorage, gameConsole, simpleSettingsProvider, simplePhraseProvider);
            game.Play();
        }
    }
}
