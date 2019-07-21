namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Languages lang = Languages.English;
            // Initializing game components
            SimpleRandomIntArrayGenerator simpleRandomLongArrayGenerator = new SimpleRandomIntArrayGenerator();
            SimpleStackDataStorage<int> simpleStackDataStorage = new SimpleStackDataStorage<int>();
            GameConsole gameConsole = new GameConsole();
            SimpleSettingsProvider simpleSettingsProvider = new SimpleSettingsProvider(lang, 5);
            SimplePhraseProvider simplePhraseProvider = new SimplePhraseProvider(lang);

            Game game = new Game(simpleRandomLongArrayGenerator, simpleStackDataStorage, gameConsole, simpleSettingsProvider, simplePhraseProvider);
            game.Play();
        }
    }
}
