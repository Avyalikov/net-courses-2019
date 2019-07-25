namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            Interfaces.IDoorsGenerator doorsGenerator = new DoorsNumGenerator();
            Interfaces.IInputOutputModule ioModule = new InputOutputModule();
            Interfaces.IPhraseProvider phraseProvider = new PhraseProvider();
            Interfaces.ISettingsProvider settingsProvider = new SettingsProvider();

            Game game = new Game(doorsGenerator, ioModule, phraseProvider, settingsProvider);
            game.Start(5);
        }
    }
}
