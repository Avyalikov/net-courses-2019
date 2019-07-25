using NumbersGame.Interfaces;

namespace NumbersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutput ioModule = new ConsoleIO();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator();

            Game game = new Game(phraseProvider, ioModule, settingsProvider, doorsNumbersGenerator);
            game.Run();            
        }
    }
}
