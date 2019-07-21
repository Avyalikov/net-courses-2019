using Doors_and_levels_game.Interfaces;
using Doors_and_levels_game.Components;
using System.Collections.Generic;

namespace Doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputModule ioModule = new ConsoleIOModule();
            IDoorsGenerater<List<ulong>> doorsGenerater = new RandomDoorGenetater();
            ISettingsProvider settingsProvider = new SettingsProvider();
            GameSettings settings = settingsProvider.Get();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settings.language);

            Game game = new Game (
                phraseProvider: phraseProvider,
                io: ioModule,
                doorsGenerater: doorsGenerater,
                settings: settings
            );

            game.Start();
        }
    }
}