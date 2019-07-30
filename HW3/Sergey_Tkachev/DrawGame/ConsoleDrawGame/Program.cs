namespace ConsoleDrawGame
{
    using ConsoleDrawGame.Classes;
    using ConsoleDrawGame.Interfaces;

    internal class Program
    {
        public static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settingsProvider);
            IInputOutput inputOutput = new ConsoleInputOutput();
            IBoard board = new Board(inputOutput, settingsProvider);

            Game game = new Game(phraseProvider, inputOutput, settingsProvider, board);

            game.Run();
        }
    }
}
