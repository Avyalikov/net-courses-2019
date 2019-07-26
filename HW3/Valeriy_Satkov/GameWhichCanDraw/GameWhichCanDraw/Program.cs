namespace GameWhichCanDraw
{
    using Components;
    using Interfaces;
    
    public class Program
    {
        public static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new JsonSettingsProvider();
            IInputOutputDevice inputOutputDevice = new ConsoleInputOutputDevice();            
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IBoard board = new DashBoard();
            IFigureProvider figureProvider = new FigureProvider();

            new Game(settingsProvider, inputOutputDevice, phraseProvider, board, figureProvider) { }.Start();
        }
    }
}
