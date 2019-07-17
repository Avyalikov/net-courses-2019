using Doors_and_levels_game.Interfaces;
using Doors_and_levels_game.Components;

namespace Doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();

            Game game = new Game (
                phraseProvider: phraseProvider
            );

            game.Start();
        }
    }
}