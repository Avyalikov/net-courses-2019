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
            SimpleSettingsProvider simpleSettingsProvider = new SimpleSettingsProvider(Languages.Russian, 5);

            Game game = new Game(simpleRandomLongArrayGenerator, simpleStackDataStorage, gameConsole, simpleSettingsProvider);
            game.Play();
        }
    }
}
