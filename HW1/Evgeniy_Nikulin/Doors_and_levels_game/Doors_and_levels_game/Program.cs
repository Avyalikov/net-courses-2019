using Doors_and_levels_game.Interfaces;
using Doors_and_levels_game.Components;
using System.Collections.Generic;

namespace Doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new JsonPhraseProvider();
            IInputOutputModule ioModule = new ConsoleIOModule();
            IDoorsGenerater<List<ulong>> doorsGenerater = new RandomDoorGenetater();

            Game game = new Game (
                phraseProvider: phraseProvider,
                io: ioModule,
                doorsGenerater: doorsGenerater
            );

            game.Start();
        }
    }
}