namespace DrawGame
{
    using DrawGame.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutput inputOutputModule = new ConsoleIO();
            IDraw drawModule = new DrawOnBoard();
            ISettingsProvider settingsProvider = new SettingsProvider();
            IBoard board = new Board();
            Game game = new Game(phraseProvider, inputOutputModule, drawModule, settingsProvider, board);
            game.Run();            
        }
    }
}
