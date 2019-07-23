using Doors_and_levels_game.Interfaces;
using Doors_and_levels_game.Components;
using System.Collections.Generic;

namespace Doors_and_levels_game
{
    class Program
    {
        static void Main(string[] args)
        {
            ISettingsProvider settingsProvider = new SettingsProvider();
            GameSettings settings = settingsProvider.Get();
            IPhraseProvider phraseProvider = new JsonPhraseProvider(settings.language);
            InputOutputModule ioModule = new ConsoleIOModule();
            IDoorsGenerater<List<ulong>> doorsGenerater = new RandomDoorGenetater();
            IStorageModule<ulong, List<ulong>> storage = new StorageModule();

            Game game = new Game (
                settings: settings,
                phraseProvider: phraseProvider,
                io: ioModule,
                doorsGenerater: doorsGenerater,
                storage: storage
            );

            game.Start();
        }
    }
}