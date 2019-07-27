using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            IPhraseProvider phraseProvider = new PhraseProviderFromJson();
            IDeviceInOut ioDevice = new ConsoleIO();
            ISettingsProvider settingsProvider = new SettingsProvider();
            INumbersArrayGenerator doorsNumbersGenerator = new Doors(settingsProvider);
           //IDoorsNumbersGenerator doorsNumbersGenerator = new DoorsNumbersGenerator(settingsProvider);

            var game = new Game(settingsProvider, ioDevice, phraseProvider, doorsNumbersGenerator);
            game.Run();
        }
    }
}
