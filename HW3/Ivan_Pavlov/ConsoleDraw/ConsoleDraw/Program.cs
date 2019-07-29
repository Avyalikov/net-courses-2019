namespace ConsoleDraw
{
    using ConsoleDraw.Interfaces;
    using ConsoleDraw.Provider;

    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game(
                settingsProvider: new JsonSettings(),
                IOProvider: new ConsoleIO(),
                phraseProvider: new JsonPhraseProvider(),
                board: new Board(),
                figureProvider: new FigureProvider());

            game.Start();
        }
    }
}
