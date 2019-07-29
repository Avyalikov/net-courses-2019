using ConsoleDrawGame.Interfaces;
using ConsoleDrawGame.Classes;

namespace ConsoleDrawGame
{
    class Program
    {
        static void Main(string[] args)
        {
           
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IInputOutput inputOutput = new ConsoleInputOutput();
            IBoard board = new Board(inputOutput);

            Game game = new Game(phraseProvider, inputOutput, settingsProvider, board);

            game.Run();
        }
    }
}
