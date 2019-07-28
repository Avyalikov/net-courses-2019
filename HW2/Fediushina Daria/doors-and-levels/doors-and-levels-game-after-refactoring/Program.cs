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
            ISettingsProvider settingsProvider = new SettingsProvider();
            IPhraseProvider phraseProvider = new PhraseProviderFromJson(settingsProvider);
            IDeviceInOut ioDevice = new ConsoleIO();            
            INumbersArrayGenerator doorsNumbersGenerator = new Doors(settingsProvider);
           

            var game = new Game(settingsProvider, ioDevice, phraseProvider, doorsNumbersGenerator);
            game.Run();
        }
    }
}
