using Doors_and_levels_game.Interfaces;
using Doors_and_levels_game.Components;

namespace Doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputModule ioModule = new ConsoleIOModule();

            Game game = new Game (
                phraseProvider: phraseProvider,
                io: ioModule
            );

            game.Start();
        }
    }
}