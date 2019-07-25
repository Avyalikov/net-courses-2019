namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            Interfaces.IDoorsGenerator doorsGenerator = new DoorsNumGenerator();
            Interfaces.IInputOutputModule ioModule = new InputOutputModule();
            Interfaces.IPhraseProvider phraseProvider = new PhraseProvider();
            Game game = new Game(doorsGenerator, ioModule, phraseProvider);
            game.Start(5);
        }
    }
}
