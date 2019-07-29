namespace Console_Draw_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Interfaces.IBoard board = new DashBoard();
            Interfaces.IFigures figures = new Figures();
            Interfaces.IInputOutputModule ioModule = new InputOutputModule();
            Interfaces.IPhraseProvider phraseProvider = new PhraseProvider();
            Interfaces.ISettingsProvider settingsProvider = new SettingsProvider();

            Game game = new Game(board, figures, ioModule, phraseProvider, settingsProvider);
            game.Start();
        }
    }
}
